using BLL.Person_entities.UserFolder;
using BLL.ViewModels;
using DAL.Data;
using Microsoft.AspNetCore.Mvc;

namespace WebProject.Controllers
{
	public class UserController : BaseController
	{
		public IActionResult View(int id)
		{
			var viewModel = new UserViewModel();
			var user = new User
			{
				UserName = "primeMentor",
				Mentor = new BLL.Person_entities.Mentor
				{
					About = "Ласкаво просимо на профіль розробників нашої платформи!У нас є багато цікавих курсів, які допоможуть вам вивчити нові навички та розширити свої знання у світі програмування та розробки. Відкриті для всіх, ці курси створені з метою надати вам можливість поглибитися у своїх інтересах і розвинути свій потенціал.\r\n\r\nПерегляньте наш профіль розробників, де ви знайдете різноманітні курси, які підійдуть для початківців і досвідчених розробників. Захоплюючі проекти та практичні завдання допоможуть вам набути необхідні навички та застосувати їх на практиці.\r\n\r\nПриєднуйтеся до нашого профілю розробників і досліджуйте світ програмування разом з нами. Ви знайдете безліч викликів та можливостей для саморозвитку. Почніть свою подорож з нашими захоплюючими курсами вже сьогодні!",
					Courses = SampleData.sampleCourses,
				}
			};
			viewModel.ShownUser = user;
			viewModel.UserMentor = user.Mentor;
			viewModel.EditProfile = false;
			/*
			 * TODO:
			 * viewModel.CommonCourses = user.Mentor.Courses
												.Where(course => course.Group
												.Students
												.Any(stud => stud.User.UserName == User.Identity.Name));
			 */
			return View(viewModel);
		}
	}
}
