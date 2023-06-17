using BLL.Educational_entities.Organization;
using BLL.Injections;
using BLL.Person_entities.UserFolder;
using BLL.ViewModels;
using DAL.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DAL.Services
{
	public class CourseControllerHelper : ICourseControllerHelper
	{
		private readonly DbContextProject _context;
		private readonly ILogger<CourseControllerHelper> _logger;

		public CourseControllerHelper(DbContextProject context, ILogger<CourseControllerHelper> logger)
		{
			_context = context;
			_logger = logger;
		}
		public async Task<int> GetAuthorId(Courses course)
		{
			// Асинхронно дістаю ментора, автора даного курсу
			var mentor = await _context.Mentors
				.FirstOrDefaultAsync(mentor => mentor.Id == course.AuthorId);
			if (mentor == null)
			{
				_logger.LogError($"Помилка! Не було знайдено ментора для курсу Id = {course.Id}!");
				return -1;
			}

			// Далі вибираю дані користувача, за яким зареєстрований цей ментор
			var mentorUser = await _context.Users
				.FirstOrDefaultAsync(user => user.Mentor != null && user.Mentor.Id == mentor.Id);
			if (mentorUser == null)
			{
				_logger.LogError($"Помилка! Не було знайдено користувача для ментора Id = {mentor.Id}");
				return -1;
			}
			_logger.LogInformation($"Визначено яким з користувачів цей ментор є: [ Id = {mentorUser.Id}, UserName = {mentorUser.UserName} ].");
			return mentorUser.Id;
		}
		public async Task<List<CourseInfoViewModel>> CreateModel(List<Courses> courses)
		{
			var list = new List<CourseInfoViewModel>();
			foreach (var course in courses)
			{
				var authorUserId = await GetAuthorId(course);
				var author = await _context.Users.FirstOrDefaultAsync(user => user.Id == authorUserId);
				list.Add(new CourseInfoViewModel
				{
					CourseInfo = course,
					AuthorNickName =  author?.UserName ?? string.Empty,
					AuthorId = authorUserId,
					Options = course.Options,
				});
			}
			return list;
		}
        /// <summary>
        /// Дістаємо усю інформацію про курс, за його id
        /// </summary>
        /// <param name="id"> id курсу</param>
        /// <returns> Модель курсу створену на основі його даних </returns>
        public async Task<CourseInfoViewModel?> GetCourseDataById (int id)
		{
            // Проходжуся по них та шукаю відповідний курс
            var course = await _context.Courses
				.Include(c => c.Options)
				.ThenInclude(o => o.AdditionalInfo)
				.FirstOrDefaultAsync(c => c.Id == id);

            _logger.LogInformation($"Було вибрано курс за Id {id}.");

            if (course == default(Courses))
            {
                //TODO: Зробити показ помилки при неіснуючому курсі
                _logger.LogError($"Помилка! Курс за Id {id} не знайдено!");
                return null;
            }
            var authorId = await GetAuthorId(course);
            var author = await _context.Users
                .FirstOrDefaultAsync(user => user.Id == authorId);
            if (author == null)
            {
                _logger.LogError($"Помилка! Автора курсу id = {id} не знайдено!");
                return null;
            }
            // Дістаю теми для курсу, з урахуванням завдань у них
            var topics = await _context.Topics
                .Include(tops => tops.Tasks)
                .Include(tops => tops.Tests)
                .Where(tops => tops.Course.Id == course.Id)
                .ToListAsync();

            _logger.LogInformation($"Дістаю теми для курса id = {course.Id} з урахуванням їх завдань.");
            return new CourseInfoViewModel
            {
                CourseInfo = course,
                AuthorNickName = author.UserName,
                AuthorId = authorId,
                Options = course.Options,
                Topics = topics
            };
        }
	}
}
