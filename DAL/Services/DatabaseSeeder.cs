using BLL.Educational_entities.Education;
using BLL.Educational_entities.Organization;
using BLL.Person_entities;
using BLL.Person_entities.UserFolder;
using DAL.Data;

namespace DAL.Services
{
	public class DatabaseSeeder
	{
		public static void SeedDatabase(DbContextProject context)
		{
			// Курс
			var course = new Courses
			{
				Name = "Python Course",
				Description = "Цей курс пропонує повний набір матеріалів та ресурсів для вивчення мови програмування Python. Він розрахований на тих, хто має мінімальний досвід у програмуванні або навіть для абсолютних початківців. Курс включає п'ять основних тем, які охоплюють широкий спектр концепцій та навичок.\nПереваги курсу:\n\t-Чіткі та лаконічні пояснення концепцій\n\t-Багато практичних вправ та завдань для закріплення матеріалу\n\t-Інтерактивні приклади та демонстрації коду\n\t-Підтримка від кваліфікованого інструктора та спільноти студентів\n\t-Гнучкий графік навчання та доступ до матеріалів у будь-який зручний час\n\tПриєднуйтесь до курсу \"Вивчення Python\" і розкрийте свій потенціал у світі програмування!",
				CreationData = DateTime.Now,
			};

			// Додаткова інформація про курс
			var options = new CourseOptions
			{
				IsPublic = true,
				Level = CourseLevel.Beginner,
				Type = CourseType.Desktop,
				Course = course,
			};
			course.Options = options;

			// Додаткові характеристики курсу
			var courseAdditionalOptions = new CourseAdditionalCharacteristics[]
			{
				new CourseAdditionalCharacteristics
				{
					Name = "Практичний",
					Options = options
				},
				new CourseAdditionalCharacteristics
				{
					Name = "Основи програмування",
					Options = options
				},
				new CourseAdditionalCharacteristics
				{
					Name = "Інформативний",
					Options = options
				}
			};
			options.AdditionalInfo = courseAdditionalOptions;

			// Створюю порожню групу для курсу
			var group = new Group
			{
				Name = "Python Course Group",
				Course = course,
			};
			course.Group = group;

			// Створюю першого юзера адміна
			var userAdmin = new User
			{
				UserName = "admin",
			};

			var hashGenerator = new HashCodeGenerator();
			//Створюю додаткову інформаю про користувача
			var adminInfo = new UserInfo
			{
				Role = UserRole.Admin,
				Email = "admin@gmail.com",
				Password = hashGenerator.GenerateHash("adminpass")
			};

			// Створюю роль ментора під нього
			var mentorAdmin = new Mentor
			{
				FirstName = "Admin",
				LastName = "OfWebSite",
				About = "Акаунт адміна для виконання робіт та тестування вебстайту",
				User = userAdmin,
				Courses = new List<Courses> { course }
			};
			course.Author = mentorAdmin;
			userAdmin.Info = adminInfo;
			userAdmin.Mentor = mentorAdmin;

			// Теми новоствореного курсу
			var topics = new List<Topic>()
			{
					new Topic
					{
						Title = "Основи мови Python",
						Description = "Ця тема ознайомить вас з основами мови програмування Python. Ви дізнаєтесь про синтаксис мови, змінні, типи даних та базові операції. Ви також навчитеся створювати та виконувати перші програми на Python.",
						Course = course
					},
					new Topic
					{
						Title = "Умовні конструкції та цикли",
						Description = "У цій темі ви дослідите умовні конструкції, такі як if-else, та цикли, такі як for та while. Ви навчитеся створювати умовні блоки коду та повторювати дії за допомогою циклів. Ви також вирішите різні завдання, що вимагають використання умов та циклів.",
						Course = course
					},
					new Topic
					{
						Title = "Функції та модулі",
						Description = "У цій темі ви дізнаєтесь про функції, які дозволяють вам організувати код у більш логічні блоки. Ви навчитеся створювати власні функції та використовувати вбудовані функції. Крім того, ви дізнаєтесь про модулі, які дозволяють організувати код у відокремлені файли та пакети для більшої структури проекту.",
						Course = course
					},
					new Topic
					{
						Title = "Робота зі списками та словниками",
						Description = "У цій темі ви дізнаєтесь про списки та словники, які є потужними структурами даних в Python. Ви навчитеся створювати та маніпулювати списками, включаючи додавання, видалення та зміну елементів. Ви також дізнаєтесь, як працювати зі словниками для зберігання та доступу до даних за допомогою ключів.",
						Course = course
					},
					new Topic
					{
						Title = "Робота з файлами та обробка виключень",
						Description = "У цій темі ви дізнаєтесь, як працювати з файлами в Python. Ви навчитеся читати дані з файлів, записувати дані до файлів та працювати з різними типами файлів, такими як текстові файли та файли CSV. Ви також ознайомитеся з обробкою виключень, що дозволяє керувати помилками під час виконання програми.",
						Course = course
					}
			};
			// Додаю один тест (для проби)
			var test = new List<Test>
			{
				new Test()
				{
					Name = "Закріплення знань",
					Description = "Цей тест дозволить вам закріпити знання та навички, які ви отримали на курсі.",
					OnTopic = topics[0]					
				}
			};
			// Додаю практичні завдання з кодом
			var assignments = new List<Assignment>
			{
				new Assignment {
					Name = "Hello, World!",
					Description = "Напишіть програму, яка виводить на екран фразу \"Привіт, світе!\".",
					MainTask = "Напишіть програму, яка використовує функцію print() для виведення тексту \"Hello, World!\" на екран.\nВи можете використати у функції print() будь-яку строку за прикладом:\n$str = \"Hey there!\"\n$print(str)\nВивід:\n$Hello, World!",
					ProgramingLanguages = "python",
					StarterCode = "#За прикладом ви можете додати у функцію фразу\nprint(\"\")",
					OnTopic = topics[0]
				},
				new Assignment {
					Name = "Обчислення площі прямокутника",
					Description ="Напишіть програму, яка запитує у користувача довжину і ширину прямокутника, а потім обчислює й виводить на екран його площу.",
					MainTask = "Напишіть програму, яка запитує у користувача довжину і ширину прямокутника за допомогою функції input(). Потім використовуйте ці значення для обчислення площі прямокутника (довжина помножена на ширину) та виведіть результат за допомогою функції print().",
					ProgramingLanguages = "python",
					OnTopic = topics[0]
				},
				new Assignment {
					Name = "Калькулятор",
					Description="Напишіть програму, яка запитує у користувача два числа і операцію (+, -, *, /), а потім виконує відповідну операцію та виводить результат на екран.",
					MainTask = "Напишіть програму, яка запитує у користувача два числа за допомогою функції input() та операцію (+, -, *, /). Залежно від введеної операції виконайте відповідну математичну операцію над цими числами. Використайте умовний оператор if або elif для визначення операції. Виведіть результат за допомогою функції print().",
					ProgramingLanguages = "python",
					OnTopic = topics[0]
				},
			};
			topics[0].Tests = test;
			topics[0].Tasks = assignments;
			course.Topics = topics;

			context.Courses.Add(course);
			context.CoursesOptions.Add(options);
			context.CoursesAdditionalCharacteristics.AddRange(courseAdditionalOptions);

			context.Topics.AddRange(topics);
			context.Tests.AddRange(test);
			context.Assignments.AddRange(assignments);

			context.Users.Add(userAdmin);
			context.UsersInfo.Add(adminInfo);
			context.Mentors.Add(mentorAdmin);

			context.SaveChanges();
		}
	}
}
