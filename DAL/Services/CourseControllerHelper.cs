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
	}
}
