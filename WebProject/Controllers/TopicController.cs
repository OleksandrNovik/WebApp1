using BLL.Educational_entities.Education;
using BLL.ViewModels;
using BLL.ViewModels.Create_Models;
using DAL.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebProject.Controllers
{
	public class TopicController : BaseController
	{
		private readonly ILogger<TopicController> _logger;
		private readonly DbContextProject _dbContext;
		public TopicController(ILogger<TopicController> logger, DbContextProject dbContext) 
		{
			_logger = logger;
			_dbContext = dbContext;
		}
		/// <summary>
		/// Показ інформації щодо теми за її id
		/// </summary>
		/// <param name="id"> Ідентифікатор теми </param>
		/// <returns> Перехід до представлення показу теми </returns>
		public async Task<IActionResult> View(int id)
		{
			var topic = await _dbContext.Topics
				.Include(t => t.Tasks)
				.Include(t => t.Tests)
				.Include(t => t.Course)
				.FirstOrDefaultAsync(topic => topic.Id == id);
			if (topic == default(Topic)) 
			{
				_logger.LogError("Помилка! Тему за id {0} не знайдено!", id);
				return NotFound();
			}
			_logger.LogInformation("На основі теми за id {0} було створено та передано модель.", id);
			return View(new TopicViewModel
			{
				TopicTitle = topic.Title,
				TopicDescription = topic.Description,
				CourseName = topic.Course.Name,
				Assignments = topic.Tasks,
				Tests = topic.Tests,
			});
		}
		/// <summary>
		/// Створення теми на курсі
		/// </summary>
		/// <param name="courseId"> Id курсу, до якого додаємо тему</param>
		/// <returns> Форму створення теми </returns>
		[HttpGet]
		[Authorize(Roles = "Mentor, Admin")]
		public IActionResult Create(int courseId)
		{
			ViewData["courseId"] = courseId;
			return View();
		}
		/// <summary>
		/// Створення теми на відповідному курсі
		/// </summary>
		/// <param name="model"> Модель отримана з форми </param>
		/// <returns> Якщо модель валідна перехід назад на редагування курсу
		/// Інакше перехід назад на форму</returns>
		public async Task<IActionResult> Create(CreateTopicModel model)
		{
            if (ModelState.IsValid)
			{
                var onCourse = await _dbContext
					.Courses
					.Include(c => c.Topics)
					.FirstOrDefaultAsync(c => c.Id == model.CourseId);
                if (onCourse == null)
                {
                    return NotFound();
                }
				var topic = new Topic
				{
					Title = model.Title,
					Description = model.Description,
					Course = onCourse
				};
				if (onCourse.Topics == null)
				{
                    onCourse.Topics = new List<Topic>();
                }
                onCourse.Topics.Add(topic);
				await _dbContext.Topics.AddAsync(topic);
				await _dbContext.SaveChangesAsync();
				return RedirectToAction("Edit", "Courses", new { id = model.CourseId });
            }
			ViewData["courseId"] = model.CourseId;
			return View(model);
		}
	}
}
