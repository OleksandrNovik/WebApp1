using BLL.Educational_entities.Organization;
using BLL.Injections;
using BLL.ViewModels;
using DAL.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
	}
}
