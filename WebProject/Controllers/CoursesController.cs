using BLL.Educational_entities.Organization;
using BLL.Injections;
using BLL.ViewModels;
using DAL.Data;
using Microsoft.AspNetCore.Mvc;

namespace WebProject.Controllers
{
	public class CoursesController : BaseController
	{
		private readonly ICourseControllerHelper _helper;

		public CoursesController(ICourseControllerHelper helper) 
		{
			_helper = helper;
		}
		//TODO: Додати використання view index для показу курсів учня, + публічних курсів як приклад, + відфільтрованих курсів, + курсів за пошуком
		public IActionResult Index()
		{
			var model = _helper.SetModel(SampleData.sampleCourses);
            return View(model);
		}
		/// <summary>
		/// Додаткова інформація про курс
		/// </summary>
		/// <param name="id"> Id курсу який треба показати </param>
		/// <returns> Представлення з відповідним курсом </returns>
		public IActionResult View(int id)
		{
			var course = SampleData.sampleCourses.FirstOrDefault(c => c.Id == id);
			if (course == default(Courses))
			{
				//TODO: Зробити показ помилки при неіснуючому курсі
				return RedirectToAction("Index", "Home");
			}
			return View(new CourseInfoViewModel { 
				CourseInfo = course,
                AuthorNickName = "primeMentor12",
				Options = course.Options,
            });
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
