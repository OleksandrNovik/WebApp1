using BLL.Person_entities.UserFolder;
using BLL.ViewModels;
using DAL.Data;
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
		/// <summary>
		/// Показ інформації про користувача
		/// </summary>
		/// <param name="id"> id користувача інформацію про якого ми дивимось </param>
		/// <param name="isViewMode"> 
		/// Чи це є просто показ, або ж налаштування профіля
		/// При значенні true буде виведено розширене view з редагуванням профіля
		/// Для користувачів, що переглядають свій профіль
		/// </param>
		/// <returns> Відповідне представлення </returns>
		public async Task<IActionResult> View(int id, bool isViewMode = false)
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
			viewModel.EditProfile = isViewMode;
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
		[HttpGet("/avatar/{userId}")]
		public IActionResult GetUserPhoto(int userId)
		{
			_logger.LogInformation($"Пошук користувача за id = {userId}.");
			var user = _dbContext.Users
				.Include(u => u.UserPhoto)
				.FirstOrDefault(u => u.Id == userId);
			if (user == null)
			{
				_logger.LogError($"Помилка у {nameof(GetUserPhoto)}!\nКористувача не знайдено!");
				return NotFound();
			}
			_logger.LogInformation($"Виявлено користувача за id = {userId}.");
			if (user.UserPhoto == null)
			{
				_logger.LogInformation("Користувач не поставив собі аватар, тому взято стандартну картинку.");
				return File("~/resources/TestPhoto.png", "image/png");
			}
			var imageType = "image/" + user.UserPhoto.FileType.ToString();
			_logger.LogInformation($"У користувача є аватар, з розширенням \"{imageType}\".");
			return File(user.UserPhoto.PhotoByteCode, imageType);
		}
	}
}
