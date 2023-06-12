using BLL.Educational_entities.Organization;
using BLL.Injections;
using BLL.ViewModels;
using BLL.ViewModels.Create_Models;
using DAL.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Xml;

namespace WebProject.Controllers
{
	public class CoursesController : BaseController
	{
		private readonly ILogger<CoursesController> _logger;
		private readonly ICourseControllerHelper _helper;
		private readonly DbContextProject _dbContext;
		public CoursesController(ILogger<CoursesController> logger, ICourseControllerHelper helper, DbContextProject context) 
		{
			_logger = logger;
			_helper = helper;
			_dbContext = context;
		}
		//TODO: Додати використання view index для показу курсів учня, + публічних курсів як приклад, + відфільтрованих курсів, + курсів за пошуком
		public async Task<IActionResult> Index()
		{
			var list = await _dbContext.Courses
				//.Include(c => c.Author)
				//.ThenInclude(m => m.User)
				.Include(c => c.Options)
				.ThenInclude(o => o.AdditionalInfo)
				.ToListAsync();

			var model = await _helper.CreateModel(list);

			if (model.Any(mod => string.IsNullOrEmpty(mod.AuthorNickName)))
			{
				_logger.LogError("Помилка! При показі списку курсів у одному виявлено помилку пошуку автора!");
				return RedirectToAction("Index", "Home");
			}
			_logger.LogInformation("Передано та створено список моделей для показу курсів.");
			ViewData["Title"] = "Публічні курси";
            return View(model);
		}
		/// <summary>
		/// Додаткова інформація про курс
		/// </summary>
		/// <param name="id"> Id курсу який треба показати </param>
		/// <returns> Представлення з відповідним курсом </returns>
		public async Task<IActionResult> View(int id)
		{
			// Проходжуся по них та шукаю відповідний курс
			var course = await _dbContext.Courses
				.Include(c => c.Options)
				.ThenInclude(o => o.AdditionalInfo)
				.FirstOrDefaultAsync(c => c.Id == id);

			_logger.LogInformation($"Було вибрано курс за Id {id}.");

			if (course == default(Courses))
			{
				//TODO: Зробити показ помилки при неіснуючому курсі
				_logger.LogError($"Помилка! Курс за Id {id} не знайдено!");
				return RedirectToAction("Index", "Home");
			}
			var authorId = await _helper.GetAuthorId(course);
			var author = await _dbContext.Users
				.FirstOrDefaultAsync(user => user.Id == authorId);
			if (author == null)
			{
				_logger.LogError($"Помилка! Автора курсу id = {id} не знайдено!");
				return RedirectToAction("Index", "Home");
			}
			// Дістаю теми для курсу, з урахуванням завдань у них
			var topics = await _dbContext.Topics
				.Include(tops => tops.Tasks)
				.Include(tops => tops.Tests)
				.Where(tops => tops.Course.Id == course.Id)
				.ToListAsync();

			_logger.LogInformation($"Дістаю теми для курса id = {course.Id} з урахуванням їх завдань.");

			var model = new CourseInfoViewModel
			{
				CourseInfo = course,
				AuthorNickName = author.UserName,
				AuthorId = authorId,
				Options = course.Options,
				Topics = topics
			};
			return View(model);
		}
		/// <summary>
		/// Метод фільтрації курсів по характеристикам
		/// </summary>
		/// <param name="filterByLevel"> Рівень складності </param>
		/// <param name="filterByType"> Типу курсу (може бути математичний, історичний, програмування і тд)</param>
		/// <param name="filterByLessons"> Кількість уроків </param>
		/// <returns> Повертається до Index view але з відфільтрованим списком </returns>
		[HttpPost]
		public IActionResult FilterCourses(string[] filterByLevel, string[] filterByType, string[] filterByLessons)
		{
			//TODO: Зробити так, щоб при фільтрації chechbox, які попередньо були нажаті зберігали статус того, що вони є нажаті
			ViewBag.fileter = filterByLevel;
			return Ok(new {filterByLevel, filterByType, filterByLessons });
			//return View("Index", _helper.SetModel(SampleData.sampleCourses));
		}
		[HttpPost]
		public IActionResult Search()
		{
			return View();
		}
		/// <summary>
		/// Створення нового курсу
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		[Authorize(Roles = "Mentor, Admin")]
		public IActionResult Create()
		{
			return View();
		}
		/// <summary>
		/// Створити новий курс
		/// </summary>
		/// <param name="course"> Модель, що прийшла з форми </param>
		/// <returns></returns>
		[HttpPost]
		[Authorize(Roles = "Mentor, Admin")]
		public async Task<IActionResult> Create(CourseCreateModel model)
		{
			if (User.Identity == null)
			{
				_logger.LogInformation("Незареєстрований користувач спробував доступитися до контенту, який вимагає реєстрації. Перехід на Log In сторінку.");
				return RedirectToAction("LogIn", "Account");
			}
			if (ModelState.IsValid)
			{
				_logger.LogInformation("Введено валідні дані про курс, тому створюється новий курс та додаткові таблиці.");
				var course = new Courses
				{
					Name = model.Name,
					Description = model.Description,
					CreationData = DateTime.Now,
                };
				var courseoptions = new CourseOptions
				{
					Course = course,
					IsPublic = model.IsPublic,
					Level = model.Level,
					Type = model.Type
				};
				if (!string.IsNullOrEmpty(model.Additional))
				{
					_logger.LogInformation("Було також додано характеристики курсу, тому початок обробки даних про них.");
					// Виділяю усі характеристики, що розділені хочаб чимось, та відсіюю ті, які можуть бути як порожні рядки
					var additionalInfo = model
						.Additional
						.Split(new char[] { ',', '.', '\n', ';', ':' })
						.Where(s => !string.IsNullOrEmpty(s));
					// Створюю та заповнюю новий список entity для додаткової інформації про курс
					var additioanlInfoList = new List<CourseAdditionalCharacteristics>();
					foreach(var info in  additionalInfo)
					{
						additioanlInfoList.Add(new CourseAdditionalCharacteristics
						{
							Name = info, Options = courseoptions
						});
					}
					// Записую та зберігаю цю інформацію
					courseoptions.AdditionalInfo = additioanlInfoList;
                    await _dbContext.CoursesAdditionalCharacteristics.AddRangeAsync(additioanlInfoList);
                }
				// Записую і додаткові характеристики курсу
                course.Options = courseoptions;
				// Шукаю серед користувачів та менторів автора курсу
				_logger.LogInformation($"Початок пошуку ментора, який буде автором цього курсу. За іменем поточного корсистувача \"{User.Identity.Name}\".");
				var creator = await _dbContext
					.Users
					.FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);
				var mentor = await _dbContext
					.Mentors
					.FirstOrDefaultAsync(m => m.User == creator);
				if (mentor == null) 
				{
                    _logger.LogError($"У контролері {nameof(CoursesController)}, методі {nameof(Create)} помилка! Ментора для користувача \"{User.Identity.Name}\" не знайдено!");
                    return NotFound();
				}
				_logger.LogInformation($"Знайдено ментора за id = {mentor.Id}.");
				course.Author = mentor;
				await _dbContext.Courses.AddAsync(course);
				await _dbContext.CoursesOptions.AddAsync(courseoptions);
				var newGroup = new Group
				{
					Course = course,
					Name = $"\"{course.Name}\" група"
				};
				await _dbContext.StudentGroups.AddAsync(newGroup);
                await _dbContext.SaveChangesAsync();
                _logger.LogInformation($"Усі дані успішно збережено. Перехід на сторінку курсів користувача.");
                return RedirectToAction("MyCourses");
			}
			return View(model);
		}
		/// <summary>
		/// Показ курсів даного користувача
		/// </summary>
		/// <returns> Показує курси створені даним ментором </returns>
		[Authorize(Roles = "Mentor, Admin")]
		public async Task<IActionResult> MyCourses()
		{
			if (User.Identity == null)
			{
				return RedirectToAction("LogIn", "Account");
			}
			// Дістаю курси даного користувача
			var courses = await _dbContext.Courses
				.Include(c => c.Options)
				.ThenInclude(o => o.AdditionalInfo)
				.Include(c => c.Author)
				.ThenInclude(m => m.User)
				.Where(c => c.Author.User.UserName == User.Identity.Name)
				.ToListAsync();

			var model = await _helper.CreateModel(courses);
			ViewData["Title"] = "Мої курси";
			return View("Index",  model);
		}
	}
}
