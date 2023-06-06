﻿using BLL.Educational_entities.Education;
using BLL.Educational_entities.Organization;

namespace DAL.Data
{
	public class SampleData
	{
		public static List<Test> sampleTests = new List<Test>
		{
			new Test()
			{
				Id = 1,
				Name = "Закріплення знань",
				Description = "Цей тест дозволить вам закріпити знання та навички, які ви отримали на курсі."
			}
		};
        public static List<Assignment> sampleTasks = new List<Assignment>
        {
            new Assignment { 
				Id = 1, 
				Name = "Hello, World!", 
				Description = "Напишіть програму, яка виводить на екран фразу \"Привіт, світе!\".", 
				MainTask = "Напишіть програму, яка використовує функцію print() для виведення тексту \"Hello, World!\" на екран.\nВи можете використати у функції print() будь-яку строку за прикладом:\n$str = \"Hey there!\"\n$print(str)\nВивід:\n$Hello, World!",
				ProgramingLanguage = "python",
				StarterCode = "#За прикладом ви можете додати у функцію фразу\nprint(\"\")",
			},
            new Assignment { 
				Id = 2, 
				Name = "Обчислення площі прямокутника", 
				Description ="Напишіть програму, яка запитує у користувача довжину і ширину прямокутника, а потім обчислює й виводить на екран його площу.",
				MainTask = "Напишіть програму, яка запитує у користувача довжину і ширину прямокутника за допомогою функції input(). Потім використовуйте ці значення для обчислення площі прямокутника (довжина помножена на ширину) та виведіть результат за допомогою функції print().",
				ProgramingLanguage = "python"
			},
            new Assignment { 
				Id = 3, Name = "Калькулятор", 
				Description="Напишіть програму, яка запитує у користувача два числа і операцію (+, -, *, /), а потім виконує відповідну операцію та виводить результат на екран.",
				MainTask = "Напишіть програму, яка запитує у користувача два числа за допомогою функції input() та операцію (+, -, *, /). Залежно від введеної операції виконайте відповідну математичну операцію над цими числами. Використайте умовний оператор if або elif для визначення операції. Виведіть результат за допомогою функції print().",
				ProgramingLanguage = "python"
			},
			new Assignment { 
				Id = 4, 
				Name = "Конвертер валют", 
				Description = "Напишіть програму, яка конвертує суму в одній валюті в еквівалентну суму в іншій валюті. Запитайте користувача про початкову суму та обидві валюти. Виведіть конвертовану суму на екран.", 
				MainTask = " Напишіть програму, яка дозволяє користувачеві конвертувати суму з однієї валюти в іншу, використовуючи актуальний курс обміну.\r\nЗавдання: Напишіть програму, яка запитує у користувача суму, яку він бажає конвертувати, а також початкову та кінцеву валюти. Використовуйте актуальний курс обміну, який ви можете задати у програмі або отримати з зовнішнього джерела (наприклад, API). Потім, використовуючи формулу конвертації, обчисліть еквівалентну суму в кінцевій валюті та виведіть результат на екран за допомогою функції print().\r\n\r\nНаприклад, користувач може ввести суму 100 USD, початкову валюту USD та кінцеву валюту EUR. Програма повинна використовувати актуальний курс обміну і обчислити, що 100 USD дорівнюють, наприклад, 85 EUR. Результат буде виведений на екран.\r\n\r\nЦя задача допоможе вам навчитися працювати зі змінними, отримувати введення від користувача, виконувати математичні операції та виводити результат на екран.",
				ProgramingLanguage = "python"
			},
			new Assignment { 
				Id = 5, 
				Name = "Робота зі списками", 
				Description="Напишіть програму, яка створює список чисел, запитує у користувача нове число та додає його до списку. Потім виведіть весь список чисел на екран.\r\n\r\nНазва: \"Календарний планувальник\"",
				MainTask = "Напишіть програму, яка створює список з кількох елементів, наприклад, чисел або рядків. Виконайте різні операції з цим списком, такі як додавання нових елементів, видалення елементів, зміна значень елементів та доступ до елементів за їх індексом. Виведіть результати цих операцій на екран за допомогою функції print().",
				ProgramingLanguage = "python"
			},
			new Assignment { 
				Id = 6, 
				Name = "Гра вгадай число", 
				Description="Напишіть програму, яка генерує випадкове число від 1 до 100, а потім запрошує користувача вгадати це число. Виведіть повідомлення про те, чи вгадано число, або надайте підказки, якщо число не вгадано.",
				MainTask = "Напишіть програму, яка генерує випадкове число з певного діапазону. Запросіть введення числа від користувача за допомогою функції input(). Порівняйте введене число з випадково згенерованим числом і надайте відповідне повідомлення користувачеві, чи він вгадав число, чи ні. Продовжуйте запитувати число до тих пір, поки користувач не вгадає. Виведіть кількість спроб, які знадобилися користувачу, для вгадування числа.",
				ProgramingLanguage = "python"
			},
		};
		public static List<Topic> pythonTopics = new List<Topic>
				{
					new Topic
					{
						Id = 1, Title = "Основи мови Python", Tasks = sampleTasks,
						Description = "Ця тема ознайомить вас з основами мови програмування Python. Ви дізнаєтесь про синтаксис мови, змінні, типи даних та базові операції. Ви також навчитеся створювати та виконувати перші програми на Python.",
						Tests = sampleTests,
					},
					new Topic
					{
						Id = 2, Title = "Умовні конструкції та цикли", Tasks = new List<Assignment>(),
						Description = "У цій темі ви дослідите умовні конструкції, такі як if-else, та цикли, такі як for та while. Ви навчитеся створювати умовні блоки коду та повторювати дії за допомогою циклів. Ви також вирішите різні завдання, що вимагають використання умов та циклів."
					},
					new Topic
					{
						Id = 3, Title = "Функції та модулі", Tasks = new List<Assignment>(),
						Description = " У цій темі ви дізнаєтесь про функції, які дозволяють вам організувати код у більш логічні блоки. Ви навчитеся створювати власні функції та використовувати вбудовані функції. Крім того, ви дізнаєтесь про модулі, які дозволяють організувати код у відокремлені файли та пакети для більшої структури проекту."
					},
					new Topic
					{
						Id = 4, Title = "Робота з файлами та обробка виключень", Tasks = new List<Assignment>(),
						Description = "У цій темі ви дізнаєтесь, як працювати з файлами в Python. Ви навчитеся читати дані з файлів, записувати дані до файлів та працювати з різними типами файлів, такими як текстові файли та файли CSV. Ви також ознайомитеся з обробкою виключень, що дозволяє керувати помилками під час виконання програми."
					},
					new Topic
					{
						Id = 5, Title = "Робота зі списками та словниками", Tasks = new List<Assignment>(),
						Description = "У цій темі ви дізнаєтесь про списки та словники, які є потужними структурами даних в Python. Ви навчитеся створювати та маніпулювати списками, включаючи додавання, видалення та зміну елементів. Ви також дізнаєтесь, як працювати зі словниками для зберігання та доступу до даних за допомогою ключів."
					}
				};

		public static List<CourseOptions> courseOptions = new List<CourseOptions>()
        {
			new CourseOptions
			{
				Id = 1,
				IsPublic = true,
				Level = CourseLevel.Advanced,
				Type = CourseType.Calculus,
				AdditionalInfo = null
			},
			new CourseOptions
			{
				Id = 2, IsPublic = true, Level = CourseLevel.Beginner, Type = CourseType.Programming,
				AdditionalInfo = new List<CourseAdditionalCharacteristics>
				{
					new CourseAdditionalCharacteristics { Name = "Основи програмування" },
					new CourseAdditionalCharacteristics { Name =  "Базами даних"},
					new CourseAdditionalCharacteristics { Name = "Лекції та практичні заняття"},
					new CourseAdditionalCharacteristics { Name = "Info1"},
				}
			},
			new CourseOptions
			{
				Id = 3, IsPublic = true, Level = CourseLevel.Intermediate, Type = CourseType.Historical,
				AdditionalInfo = new List<CourseAdditionalCharacteristics>
				{
					new CourseAdditionalCharacteristics { Name = "Info1" },
					new CourseAdditionalCharacteristics { Name =  "Info2"},
				}
			},
			new CourseOptions
			{
				Id = 4, IsPublic = true, Level = CourseLevel.Intermediate, Type = CourseType.Geographical,
				AdditionalInfo = new List <CourseAdditionalCharacteristics>
				{
					new CourseAdditionalCharacteristics { Name = "Info1" },
					new CourseAdditionalCharacteristics { Name = "Info2"},
					new CourseAdditionalCharacteristics { Name = "Info3"},
				}
			}
		};
		public static List<Courses> sampleCourses = new List<Courses>
		{
			new Courses {
				Id = 1,
				Name = "First course",
				Description = "\"Архітектура та дизайн інтер'єрів\" - це курс, який досліджує основні концепції та принципи архітектури та дизайну інтер'єру. У цьому курсі студенти дізнаються про різні стилі та техніки дизайну, а також про історію та розвиток архітектури та дизайну інтер'єру від античності донаших днів. Вони вивчають техніки створення креслень, розуміння пропорцій та простору, а також вибір кольорів та матеріалів для створення гармонійного інтер'єру. Студенти також досліджують сучасні тенденції в дизайні інтер'єру та вивчають інноваційні матеріали та техніки, які можуть бути використані для створення унікальних інтер'єрів.",
				Options = courseOptions[0]
			},
			new Courses {
				Id = 2,
				Name = "Python Course",
				Description = "Цей курс пропонує повний набір матеріалів та ресурсів для вивчення мови програмування Python. Він розрахований на тих, хто має мінімальний досвід у програмуванні або навіть для абсолютних початківців. Курс включає п'ять основних тем, які охоплюють широкий спектр концепцій та навичок.\nПереваги курсу:\n\t-Чіткі та лаконічні пояснення концепцій\n\t-Багато практичних вправ та завдань для закріплення матеріалу\n\t-Інтерактивні приклади та демонстрації коду\n\t-Підтримка від кваліфікованого інструктора та спільноти студентів\n\t-Гнучкий графік навчання та доступ до матеріалів у будь-який зручний час\n\tПриєднуйтесь до курсу \"Вивчення Python\" і розкрийте свій потенціал у світі програмування!",
				Options = courseOptions[1],
				Topics = pythonTopics
			},
			new Courses {
				Id = 3,
				Name = "History Course",
				Description = "Історія України - це курс, який допоможе вам дізнатися більше про історію вашої країни. Ви дізнаєтеся про найважливіші події, які вплинули на розвиток України, від давніх часів до сьогодення. На цьому курсі ви дізнаєтеся про історію української культури, національних традицій та звичаїв.",
				Options = courseOptions[2]
			},
			new Courses {
				Id = 4,
				Name = "Test course",
				Options = courseOptions[3]
			}
		};
	}
}
