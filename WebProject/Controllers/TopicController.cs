using BLL.Educational_entities.Education;
using BLL.ViewModels;
using DAL.Data;
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
	}
}
