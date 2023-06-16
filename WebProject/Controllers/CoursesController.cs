using BLL.Educational_entities.Organization;
using BLL.Injections;
using BLL.ViewModels;
using BLL.ViewModels.Create_Models;
using DAL.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebProject.Controllers
{
	public class CoursesController : BaseController
	{
		private readonly ILogger<CoursesController> _logger;
		private readonly ICourseControllerHelper _helper;
		private readonly DbContextProject _dbContext;
		private static List<CourseInfoViewModel> currentCourseList;
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
				.Include(c => c.Topics)
				.ToListAsync();

            currentCourseList = await _helper.CreateModel(list);

			if (currentCourseList.Any(mod => string.IsNullOrEmpty(mod.AuthorNickName)))
			{
				_logger.LogError("Помилка! При показі списку курсів у одному виявлено помилку пошуку автора!");
				return RedirectToAction("Index", "Home");
			}
			_logger.LogInformation("Передано та створено список моделей для показу курсів.");
			ViewData["Title"] = "Публічні курси";
			ViewData["IsEditModel"] = false;
			ViewData["CheckBoxes"] = true;
            return View(currentCourseList);
		}
		/// <summary>
		/// Додаткова інформація про курс
		/// </summary>
		/// <param name="id"> Id курсу який треба показати </param>
		/// <returns> Представлення з відповідним курсом </returns>
		public async Task<IActionResult> View(int id)
		{
			// Дістаю дані курсу за його id
			var model = await _helper.GetCourseDataById(id);
			if (model == null)
			{
				_logger.LogError($"Помилка! Курс за id = {id} не знайдено!");
				return NotFound();
			}
			_logger.LogInformation($"Знайдено курс за id = {id}. Створено модель та показано у режимі перегляду.");
            return View(model);
		}
		/// <summary>
		/// Метод фільтрації курсів по характеристикам
		/// </summary>
		/// <param name="filterByLevel"> Рівень складності </param>
		/// <param name="filterByType"> Типу курсу (може бути математичний, історичний, програмування і тд)</param>
		/// <param name="filterByLessons"> Кількість уроків </param>
		/// <param name="isEdit"> Чи переглядається у режимі редагування </param>
		/// <returns> Повертається до Index view але з відфільтрованим списком </returns>
		[HttpPost]
		public IActionResult FilterCourses(string[] filterByLevel, string[] filterByType, string[] filterByLessons, string isEdit)
		{
            //TODO: Зробити так, щоб при фільтрації chechbox, які попередньо були нажаті зберігали статус того, що вони є нажаті
            ViewData["Title"] = "Знайдені курси";
			ViewData["IsEditModel"] = bool.Parse(isEdit);
			IEnumerable<CourseInfoViewModel> collection = currentCourseList;			
			// Задано фільтр для рівня курсу
			if (filterByLevel.Length != 0)
			{
				collection = collection
                    .Where(model => filterByLevel.Contains(model.Options.Level.ToString()));
			}
			// Задано фільр для типу курсів
			if (filterByType.Length != 0)
			{
				collection = collection
					.Where(model => filterByType.Contains(model.Options.Type.ToString()));
            }
			// Задано фільтр на к-сть уроків
			if (filterByLessons.Length != 0)
			{
				foreach(string condition in filterByLessons)
				{
					switch (condition)
					{
						case "0 - 4":
							collection = collection.Where(model => model.CourseInfo.Topics.Count < 5);
							break;

						case "5 - 10":
                            collection = collection.Where(model => model.CourseInfo.Topics.Count > 4 && model.CourseInfo.Topics.Count < 11);
                            break;

						case "10+":
                            collection = collection.Where(model => model.CourseInfo.Topics.Count > 10);
                            break;

						default: 
							break;
					}
                }
            }
			_logger.LogInformation("Виконано фільтрацію курсів.");
            return View("Index", collection.ToList());
		}
		/// <summary>
		/// Метод пошуку за фразою з поля введення
		/// </summary>
		/// <param name="searchRequest"> Фраза, яка була у пошуковому полі</param>
		/// <param name="isEdit"> Чи це режим редагування </param>
		/// <returns> Список курсів, який має у собі лише курси, що мають в назві фразу</returns>
		[HttpPost]
		public IActionResult Search(string searchRequest, string isEdit)
		{
            ViewData["Title"] = "Знайдені курси";
            ViewData["IsEditModel"] = bool.Parse(isEdit);
            currentCourseList = currentCourseList
				.Where(model => model.CourseInfo.Name.Contains(searchRequest))
				.ToList();
			_logger.LogInformation("Виконано пошук курсів.");
            return View("Index", currentCourseList);
		}
		/// <summary>
		/// Сортування за певною умовою
		/// </summary>
		/// <param name="sortOrder"> Умова сортування </param>
		/// <param name="isEdit"> Чи режим редагування </param>
		/// <returns>Відсортований список </returns>
		public IActionResult OrderCourses(string sortOrder, bool isEdit)
		{
            ViewData["Title"] = "Відсортовані курси";
			ViewData["SortByName"] = string.IsNullOrEmpty(sortOrder) ? "NameDesc" : "";
			ViewData["SortByNickName"] = sortOrder == "NickName" ? "NickNameDesc" : "NickName";
			ViewData["SortByTheme"] = sortOrder == "Topic" ? "TopicDesc" : "Topic";
            ViewData["IsEditModel"] = isEdit;

            IEnumerable<CourseInfoViewModel> collection = currentCourseList;

			switch (sortOrder)
			{
				case "NameDesc":
					collection = collection.OrderByDescending(model => model.CourseInfo.Name);
					break;
				case "NickName":
					collection = collection.OrderBy(model => model.AuthorNickName);
					break;
				case "NickNameDesc":
					collection = collection.OrderByDescending(model => model.AuthorNickName); 
					break;
				case "Topic":
					collection = collection.OrderBy(model => model.CourseInfo.Topics.Count);
					break;
				case "TopicDesc":
                    collection = collection.OrderByDescending(model => model.CourseInfo.Topics.Count);
                    break;
				default:
                    collection = collection.OrderBy(model => model.CourseInfo.Name);
                    break;

            }
			currentCourseList = collection.ToList();
            return View("Index", currentCourseList);
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
        /// <param name="model"> Модель, що прийшла з форми </param>
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
				.Include(c => c.Topics)
				.Include(c => c.Options)
				.ThenInclude(o => o.AdditionalInfo)
				.Include(c => c.Author)
				.ThenInclude(m => m.User)
				.Where(c => c.Author.User.UserName == User.Identity.Name)
				.ToListAsync();

			currentCourseList = await _helper.CreateModel(courses);
			ViewData["Title"] = "Мої курси";
            ViewData["IsEditModel"] = true;
            return View("Index", currentCourseList);
		}
        /// <summary>
        /// Редагувати курс
        /// </summary>
        /// <param name="id"> id курсу, який редагується </param>
        /// <returns> Форма редагування курсу</returns>
        [Authorize(Roles = "Mentor, Admin")]
        public async Task<IActionResult> Edit(int id)
		{
			if (User.Identity == null)
			{
				_logger.LogInformation("Незареєстрований користувач спробував доступитися до контенту, який вимагає реєстрації. Перехід на Log In сторінку.");
				return RedirectToAction("LogIn", "Account");
			}
            var model = await _helper.GetCourseDataById(id);
            if (model == null)
            {
				_logger.LogError($"Помилка! Курс за id = {id} не знайдено!");
				return NotFound();
            }
			_logger.LogInformation($"Знайдено курс за id = {id}. Створено модель та показано у режимі редагування.");
			return View(model);
		}
		/// <summary>
		/// Редагуємо вибраний курс
		/// </summary>
		/// <param name="model"> Модель отримана з форми </param>
		/// <returns> Повернення до форми, якщо модель невалідна </returns>
		[HttpPost]
		[Authorize(Roles = "Mentor, Admin")]
		public async Task<IActionResult> Edit(CourseInfoViewModel model)
		{
			var thisCourse = await _dbContext
					.Courses
					.FirstOrDefaultAsync(c => c.Id == model.CourseId);
			// Курс не знайдено - це помилка 
			if (thisCourse == null)
			{
				_logger.LogError($"Помилка! Курс за id = {model.CourseId} не знайдено!");
				return NotFound();
			}
			_logger.LogInformation($"Знайдено курс за id = {model.CourseId}. Перевірка моделі на валідність.");
			if (ModelState.IsValid)
			{ 
				// Інформацію про курс якось змінено
				if (
					thisCourse.Description != model.CourseDescription
					|| thisCourse.Name != model.CourseName
					)
				{
					_logger.LogInformation($"Модель є валідною та у ній змінена інформація, змінюється і редагований курс.");
					thisCourse.Description = model.CourseDescription;
					thisCourse.Name = model.CourseName;
					await _dbContext.SaveChangesAsync();
					return RedirectToAction("MyCourses");
				}
			}
			_logger.LogInformation($"Модель невалідна збираються необхідні дані для її повторного показу.");
			var options = await _dbContext
				.CoursesOptions
				.Include(o => o.AdditionalInfo)
				.FirstOrDefaultAsync(o => o.Id == thisCourse.OptionsId);
			var topics = await _dbContext
				.Topics
				.Include(t => t.Tasks)
				.Include(t => t.Tests)
				.Where(t => t.Course.Id == thisCourse.Id)
				.ToListAsync();
			if(options == null)
			{
				_logger.LogError($"Помилка! Курс за id = {model.CourseId} не має прив'язаних до нього характеристик!");
				return NotFound();
			}
			model.Options = options;
			model.Topics = topics;
			return View(model);
		}

    }
}
