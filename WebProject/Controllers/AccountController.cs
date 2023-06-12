using BLL.Injections;
using BLL.Person_entities;
using BLL.Person_entities.UserFolder;
using BLL.ViewModels;
using DAL.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace WebProject.Controllers
{
	public class AccountController : BaseController
	{
		private readonly ILogger<AccountController> _logger;
		private readonly DbContextProject _dbContext;
		private readonly IHashCodeGenerator _hashGenerator;

		public AccountController(ILogger<AccountController> logger, DbContextProject dbContext, IHashCodeGenerator hashGenerator)
		{
			_logger = logger;
			_dbContext = dbContext;
			_hashGenerator = hashGenerator;
		}
		/// <summary>
		/// Показ форми для входу в акаунт
		/// </summary>
		/// <returns> View з входом </returns>
		[HttpGet]
		public IActionResult LogIn()
		{
			return View();
		}
		/// <summary>
		/// Обробка даних з форми входу
		/// </summary>
		/// <param name="model"> Отримані дані </param>
		/// <returns> Повертається або до форми, або на сторінку курсів </returns>
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> LogIn(LogInViewModel model)
		{
			if (ModelState.IsValid)
			{
				_logger.LogInformation($"Спроба знайти користувача за іменем {model.UserName}.");
				var user = await _dbContext.Users
					.Include(u => u.Info)
					.FirstOrDefaultAsync(
					u => u.UserName == model.UserName &&
					u.Info.Password == _hashGenerator.GenerateHash(model.Password)
					);
				// Користувача знайдено
				if (user != null)
				{
					await Authenticate(user);
					_logger.LogInformation($"Користувач {user.UserName} був успішно аутентифікований.");
					return RedirectToAction("Index", "Courses");
				}
				// Якщо не знайдено повертаємось до форми
				ModelState.AddModelError("", "Неправильний логін або(і) пароль");
				_logger.LogInformation($"Користувача {model.UserName} не знайдено, валідацію відхилено.");
			}
			return View(model);
		}
		/// <summary>
		/// Аутентифікація користувача, визначення ролі та імені користувача
		/// </summary>
		/// <param name="user"> Користувач </param>
		/// <returns></returns>
		private async Task Authenticate(User user)
		{
			var claims = new List<Claim>()
			{
				// Ставлю клейму для користувача як його ім'я
				new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserName),
				// Одна з клейм - це роль користувача, яка і визначає можливі дії на сайті
				new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Info.Role.ToString()),
			};
			// Ідентифікація користувача
			var id = new ClaimsIdentity(
				claims,
				"ApplicationCookie",
				ClaimsIdentity.DefaultNameClaimType,
				ClaimsIdentity.DefaultRoleClaimType
				);
			// Виконання входу
			await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
		}

		[HttpGet]
		public IActionResult SignUpStud()
		{
			return View();
		}
		[HttpGet]
		public IActionResult SignUpMentor()
		{
			return View();
		}
		/// <summary>
		/// Реєстрація користувача 
		/// </summary>
		/// <param name="model"> Дані з форми </param>
		/// <returns> Залежно від валідації моделі переходимо на форму або на показ курсів </returns>
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> SignUp(SignUpViewModel model)
		{
			string view = model.Role == 0 ? "SignUpStud" : "SignUpMentor";
			if (ModelState.IsValid)
			{
				var user = await _dbContext.Users
					.FirstOrDefaultAsync(u => u.UserName == model.UserName);
				// Користувача під нікнеймом не існує
				if (user == null)
				{
					// Можна додавати новго користувача
					var newUser = new User
					{
						UserName = model.UserName,
					};
					// Якщо користувач реєструється як студент
					if (model.Role == UserRole.Student)
					{
						var student = new Student
						{
							FirstName = model.FirstName,
							LastName = model.LastName,
							User = newUser
						};
						newUser.Student = student;
						await _dbContext.Students.AddAsync(student);
					}
					// Будь який ментор або адмін є ментором
					else
					{
						var mentor = new Mentor
						{
							FirstName = model.FirstName,
							LastName = model.LastName,
							About = model.About,
							User = newUser
						};
						newUser.Mentor = mentor;
						await _dbContext.Mentors.AddAsync(mentor);
					}
					// Створюю інформаю
					var info = new UserInfo
					{
						Email = model.Email,
						Password = _hashGenerator.GenerateHash(model.Password),
						OnUser = newUser,
						Role = model.Role
					};
					newUser.Info = info;
					await _dbContext.Users.AddAsync(newUser);
					await _dbContext.UsersInfo.AddAsync(info);
					await _dbContext.SaveChangesAsync();
					await Authenticate(newUser);
					return RedirectToAction("Index", "Courses");
				}
				ModelState.AddModelError("", $"Користувач під іменем {model.UserName} вже існує");
			}
			return View(view, model);
		}
		/// <summary>
		/// Вихід з акаунта
		/// </summary>
		/// <returns> Перехід на головну сторінку </returns>
		public async Task<IActionResult> LogOut()
		{
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			return RedirectToAction("Index", "Home");
		}
	}
}
