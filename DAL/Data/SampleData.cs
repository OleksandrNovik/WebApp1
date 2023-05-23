using BLL.Educational_entities.Education;
using BLL.Educational_entities.Organization;

namespace DAL.Data
{
    public class SampleData
    {
        public static List<Assignment> sampleTasks = new List<Assignment>
        {
            new Assignment { Id = 1, Name = "FirstTask", Description = "This is the first assignment!"},
            new Assignment { Id = 2, Name = "2"},
            new Assignment { Id = 3, Name = "Assignment 3", Description="DDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDD"},
        };
        public static List<Courses> sampleCourses = new List<Courses>
        {
            new Courses {
                Id = 1,
                Name = "First course",
                Description = "\"Архітектура та дизайн інтер'єрів\" - це курс, який досліджує основні концепції та принципи архітектури та дизайну інтер'єру. У цьому курсі студенти дізнаються про різні стилі та техніки дизайну, а також про історію та розвиток архітектури та дизайну інтер'єру від античності донаших днів. Вони вивчають техніки створення креслень, розуміння пропорцій та простору, а також вибір кольорів та матеріалів для створення гармонійного інтер'єру. Студенти також досліджують сучасні тенденції в дизайні інтер'єру та вивчають інноваційні матеріали та техніки, які можуть бути використані для створення унікальних інтер'єрів.",
                Options = new CourseOptions
                {
                    Id = 1, IsPublic = true, Level = CourseLevel.Advanced, Type = CourseType.Calculus, AdditionalInfo = null
                }
            },
            new Courses {
                Id = 2,
                Name = "Python Course",
                Description = "Цей курс пропонує повний набір матеріалів та ресурсів для вивчення мови програмування Python. Він розрахований на тих, хто має мінімальний досвід у програмуванні або навіть для абсолютних початківців. Курс включає п'ять основних тем, які охоплюють широкий спектр концепцій та навичок.\nПереваги курсу:\n\t-Чіткі та лаконічні пояснення концепцій\n\t-Багато практичних вправ та завдань для закріплення матеріалу\n\t-Інтерактивні приклади та демонстрації коду\n\t-Підтримка від кваліфікованого інструктора та спільноти студентів\n\t-Гнучкий графік навчання та доступ до матеріалів у будь-який зручний час\n\tПриєднуйтесь до курсу \"Вивчення Python\" і розкрийте свій потенціал у світі програмування!",
                Options = new CourseOptions
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
                Topics = new List<Topic>
                {
                    new Topic
                    {
                        Id = 1, Title = "Основи мови Python", Tasks = new List<Assignment>(), 
                        Description = "Ця тема ознайомить вас з основами мови програмування Python. Ви дізнаєтесь про синтаксис мови, змінні, типи даних та базові операції. Ви також навчитеся створювати та виконувати перші програми на Python."
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
				}
            },
            new Courses {
                Id = 3, 
                Name = "History Course", 
                Description = "Історія України - це курс, який допоможе вам дізнатися більше про історію вашої країни. Ви дізнаєтеся про найважливіші події, які вплинули на розвиток України, від давніх часів до сьогодення. На цьому курсі ви дізнаєтеся про історію української культури, національних традицій та звичаїв.",
                Options = new CourseOptions
                {
                    Id = 3, IsPublic = true, Level = CourseLevel.Intermediate, Type = CourseType.Historical, 
                    AdditionalInfo = new List<CourseAdditionalCharacteristics>
                    {
                        new CourseAdditionalCharacteristics { Name = "Info1" },
                        new CourseAdditionalCharacteristics { Name =  "Info2"},
                    }
                }
            },
            new Courses {
                Id = 4, 
                Name = "Test course",
                Options = new CourseOptions
                {
                    Id = 4, IsPublic = true, Level = CourseLevel.Intermediate, Type = CourseType.Geographical, 
                    AdditionalInfo = new List <CourseAdditionalCharacteristics> 
                    {
                        new CourseAdditionalCharacteristics { Name = "Info1" },
                        new CourseAdditionalCharacteristics { Name = "Info2"},
                        new CourseAdditionalCharacteristics { Name = "Info3"},
                    }
                }
            }
        };
    }
}
