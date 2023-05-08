using BLL.Educational_entities.Organization;
using BLL.ViewModels;
using DAL.Data;
using Microsoft.AspNetCore.Mvc;

namespace WebProject.Controllers
{
	public class CoursesController : BaseController
	{
		//TODO: Додати використання view index для показу курсів учня, + публічних курсів як приклад, + відфільтрованих курсів, + курсів за пошуком
		public IActionResult Index()
		{
			var list = new List<CourseInfoViewModel>();
			foreach (var item in SampleData.sampleCourses) 
			{
				list.Add(new CourseInfoViewModel
				{
					CourseName = item.Name,
					CourseDescription = item.Description,
					AuthorNickName = "primeMentor12",
					IsPublic = true,
					AdditionalInfo = new string[] {"Info1", "Info2", "Info3" },
				});
			}

			list[0].LevelInfo = CourseLevel.Advanced;
			list[0].TypeInfo = CourseType.Calculus;

            list[1].LevelInfo = CourseLevel.Beginner;
            list[1].TypeInfo = CourseType.Programming;

            list[2].LevelInfo = CourseLevel.Intermediate;
            list[2].TypeInfo = CourseType.Historical;

            list[3].LevelInfo = CourseLevel.Intermediate;
            list[3].TypeInfo = CourseType.Language;

            return View(list);
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
			return Ok(filterByLessons);
		}
		[HttpPost]
		public IActionResult Search()
		{
			return View();
		}
	}
}
