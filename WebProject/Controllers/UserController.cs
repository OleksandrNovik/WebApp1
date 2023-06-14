using BLL.Person_entities.UserFolder;
using BLL.ViewModels;
using DAL.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebProject.Controllers
{
	public class UserController : BaseController
	{
		private readonly ILogger<UserController> _logger;
		private readonly DbContextProject _dbContext;

		public UserController(ILogger<UserController> logger, DbContextProject dbContext) 
		{
			_logger = logger;
			_dbContext = dbContext;
		}
		public async Task<IActionResult> Edit(int? id)
		{
			if (User.Identity == null)
			{
				return RedirectToAction("LogIn", "Account");
			}
			User user;
			if (id == null)
			{
				user = await _dbContext.Users
					.Include(u => u.UserPhoto)
                    .Include(u => u.Mentor)
					.ThenInclude(m => m.Courses)
					.ThenInclude(c => c.Options)
					.ThenInclude(o => o.AdditionalInfo)
                    .FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);
			}
			else
			{
                user = await _dbContext.Users
					.Include(u => u.UserPhoto)
					.Include(u => u.Mentor)
					.ThenInclude(m => m.Courses)
					.ThenInclude(c => c.Options)
					.ThenInclude(o => o.AdditionalInfo)
					.FirstOrDefaultAsync(u => u.Id == id);
            }
			return View("View", new UserViewModel
			{
				ShownUser = user,
				UserMentor = user.Mentor,
				EditProfile = true,
			});
		}
		/// <summary>
		/// Показ інформації про користувача
		/// </summary>
		/// <param name="id"> id користувача інформацію про якого ми дивимось </param>
		/// Чи це є просто показ, або ж налаштування профіля
		/// При значенні true буде виведено розширене view з редагуванням профіля
		/// Для користувачів, що переглядають свій профіль
		/// </param>
		/// <returns> Відповідне представлення </returns>
		public async Task<IActionResult> View(int id)
		{
			var viewModel = new UserViewModel();
			var user = await _dbContext.Users
				.Include(u => u.UserPhoto)
				.Include(u => u.Mentor)
				.ThenInclude(m => m.Courses)
				.ThenInclude(c => c.Options)
				.ThenInclude(o => o.AdditionalInfo)
				.FirstOrDefaultAsync(us => us.Id == id);

			if (user == null)
			{
				return NotFound();
			}

			viewModel.ShownUser = user;
			viewModel.UserMentor = user.Mentor;
			viewModel.EditProfile = user.UserName == User.Identity?.Name;
			/*
			 * TODO:
			 * viewModel.CommonCourses = user.Mentor.Courses
												.Where(course => course.Group
												.Students
												.Any(stud => stud.User.UserName == User.Identity.Name));
			 */
			return View(viewModel);
		}
		/// <summary>
		/// Метод який повертає фото для тегу <img> при запиті
		/// </summary>
		/// <returns> Фото користувача </returns>
		[HttpGet("/avatar/{userName}")]
		public IActionResult GetUserPhoto(string userName)
		{
			_logger.LogInformation($"Пошук користувача за ім\'я = {userName}.");
			var user = _dbContext.Users
				.Include(u => u.UserPhoto)
				.FirstOrDefault(u => u.UserName == userName);
			if (user == null)
			{
				_logger.LogError($"Помилка у {nameof(GetUserPhoto)}!\nКористувача не знайдено!");
				return NotFound();
			}
			_logger.LogInformation($"Виявлено користувача за ім\'я = {userName}.");
			if (user.UserPhoto == null)
			{
				_logger.LogInformation("Користувач не поставив собі аватар, тому взято стандартну картинку.");
				return File("~/resources/UserImg.png", "image/png");
			}
			var imageType = "image/" + user.UserPhoto.FileType.ToString();
			_logger.LogInformation($"У користувача є аватар, з розширенням \"{imageType}\".");
			return File(user.UserPhoto.PhotoByteCode, imageType);
		}
		[HttpPost]
		[Authorize]
		public async Task<IActionResult> TaskSaveChanges(string selectedTheme, string editor, string language, int taskId)
		{
			if (User.Identity == null)
			{
				return Json(new { success = false });
			}
			var user = await _dbContext
				.Users
				.Include(u => u.Info)
				.Include(u => u.Student)
				.FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);
            var task = await _dbContext
				.Assignments
				.FirstOrDefaultAsync(t => t.Id == taskId);
            
			if (user == null || user.Student == null || task == null)
			{
                return Json(new { success = false });
            }

			// Запам'ятовую вибрану користувачем тему редактора
			user.Info.EditorTheme = selectedTheme;

            var works = await _dbContext
				.StudentWorks
				.Include(w => w.OnTask)
				.Where(w => w.WorkAuthor == user.Student)
				.ToListAsync();
			
			if (works == null)
			{
				works = new List<BLL.Educational_entities.Education.Work> ();
			}
			// Якщо у таблиці уже є елемент який відповідає за роботу на цьому таску
			var currentWork = works.FirstOrDefault(w => w.OnTask.Id == taskId);
			if (currentWork != null)
			{
				currentWork.Code = editor;
				currentWork.ProgramingLanguage = language;
				currentWork.SubmitDate = DateTime.Now;
			}
			// Якщо немає, то додаємо його
			else
			{
				await _dbContext.StudentWorks.AddAsync(new BLL.Educational_entities.Education.Work
				{
					Status = BLL.Educational_entities.Education.WorkStatus.NotComplited,
					Code = editor,
					UserId = user.Id,
					ProgramingLanguage = language,
					WorkAuthor = user.Student,
					OnTask = task,
					SubmitDate = DateTime.Now,
				});
            }
            await _dbContext.SaveChangesAsync();
			return Json(new { success = true });
		}
	}
}
