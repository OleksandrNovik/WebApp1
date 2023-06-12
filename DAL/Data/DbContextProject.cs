using BLL.Educational_entities.Education;
using BLL.Educational_entities.Education.TestFolder.TestTypes;
using BLL.Educational_entities.Organization;
using BLL.Person_entities;
using BLL.Person_entities.UserFolder;
using DAL.Services;
using Microsoft.EntityFrameworkCore;

namespace DAL.Data
{
	public class DbContextProject : DbContext
	{
		public DbContextProject(DbContextOptions<DbContextProject> options) : base(options) 
		{
			// Якщо база даних уже створена то проігнорується
			if (Database.EnsureCreated())
			{
				// Інакше при новому створенні бази даних вона зразу ж заповниться стартовими даними
				DatabaseSeeder.SeedDatabase(this);
			}
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// Задаю one-to-many взаємодію ментора і курсів,
			// Якщо ментор буде видалений то всі курси також видалятьс яавтоматично
			modelBuilder.Entity<Mentor>()
				.HasMany(ment => ment.Courses)
				.WithOne(cor => cor.Author)
				.HasForeignKey(cor => cor.AuthorId)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<Courses>()
				.HasOne(c => c.Author)
				.WithMany(m => m.Courses)
				.HasForeignKey(c => c.AuthorId)
				.OnDelete(DeleteBehavior.NoAction);

			// Один курс має одну групу при видаленні курсу видаляється група
			modelBuilder.Entity<Courses>()
				.HasOne(course => course.Group)
				.WithOne(group => group.Course)
				.HasForeignKey<Courses>(course => course.GroupId)
				.OnDelete(DeleteBehavior.Cascade);

			// Один курс має один рядок в таблиці з додатковою інформацією
			// Видалення курсу - видалення інформації
			modelBuilder.Entity<Courses>()
				.HasOne(course => course.Options)
				.WithOne(options => options.Course)
				.HasForeignKey<Courses>(c => c.OptionsId)
				.OnDelete(DeleteBehavior.Cascade);

			// Один до одного у юзера до ментора, якщо видалиться ментор, то видаляється юзер
			modelBuilder.Entity<User>()
				.HasOne(u => u.Mentor)
				.WithOne(m => m.User)
				.HasForeignKey<User>(u => u.MentorId)
				.OnDelete(DeleteBehavior.Cascade);

			// Один студент представляється одним користувачем, при видаленні одного видаляється й інший
			modelBuilder.Entity<User>()
				.HasOne(u => u.Student)
				.WithOne(s => s.User)
				.HasForeignKey<User>(u => u.StudentId)
				.OnDelete(DeleteBehavior.Cascade);

			// Кожен користувач має інформацію про аватар
			// коли видаляється користувач - видаляється і аватар
			modelBuilder.Entity<User>()
				.HasOne(u => u.UserPhoto)
				.WithOne(p => p.User)
				.HasForeignKey<User>(u => u.PhotoId)
				.OnDelete(DeleteBehavior.Cascade);

			base.OnModelCreating(modelBuilder);
		}

		/// Person entities

		// Таблиця користувачів
		public DbSet<User> Users { get; set; }

		// Додаткова інформація про користувача
		public DbSet<UserInfo> UsersInfo { get; set; }
		
		// Для збереження аватарок користувачів
		public DbSet<UserPhoto> UserPhotos { get; set; }

		// Таблиця користувачів в ролі студента (студентів)
		public DbSet<Student> Students { get; set; }

		// Таблиця менторів (викладачів)
		public DbSet<Mentor> Mentors { get; set; }

		/// Organization
		
		// Таблиця усіх курсів
		public DbSet<Courses> Courses { get; set; }

		// Таблиця додаткової інформації про кожен курс
		public DbSet<CourseOptions> CoursesOptions { get; set; }

		// Таблиця характеристик курсу (тобто кастомних які йому надав ментор)
		// Це може бути щось, що допоможе швидше знайти курс серед інших
		// Вони виводяться після основних характеристик курсу
		public DbSet<CourseAdditionalCharacteristics> CoursesAdditionalCharacteristics { get; set; }

        // Групи студентів
        public DbSet<Group> StudentGroups { get; set; }

        /// Educational

        // Таблиця розгорнутого практичного завдання з кодом
        public DbSet<Assignment> Assignments { get; set; } 

		// Таблиця оцінок за завдання різного типу
		public DbSet<Mark> Marks { get; set; }

		// Таблиця усіх тем на вебсайті (з усіх курсів)
		public DbSet<Topic> Topics { get; set; } 

		// Таблиця робіт студентів (або просто людей які пройшли завдання з курсу)
		// У ній зберігаються уся інформація про роботу з завданням з кодом
		// А також результат роботи (оцінка ментора)
		public DbSet<Work> StudentWorks { get; set; }
		
		// Таблиця тестових завдань (на вільну тему)
		public DbSet<Test> Tests { get; set; }
		
		// Таблиця всіх запитань до всіх тестів
		public DbSet<Questions> Questions { get; set; }

		// Таблиця яка представляє усі відповіді (та віріанти відповідей на тести)
		public DbSet<Answers> Answers { get; set; }

		// Таблиця де зберігатиметься інформація про відповіді студентів
		public DbSet<StudentAnswer> StudentsAnswers { get; set; }

		// Тести з одною правильною відповіддю
		public DbSet<OneAnswerTest> OneAnswerTests { get; set; }

		// Тести з декількома правильними відповідями
		public DbSet<ManyAnswerTest> ManyAnswerTests { get; set; }

		// Тести з розгорнутою відповіддю
		public DbSet<FullAnswerTest> FullAnswerTests { get; set; }
	}
}
