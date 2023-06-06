using BLL.Educational_entities.Education;
using BLL.ViewModels;
using DAL.Data;
using Microsoft.AspNetCore.Mvc;

namespace WebProject.Controllers
{
	public class TopicController : BaseController
	{
		public IActionResult View(int id)
		{
			var topic = SampleData.pythonTopics.FirstOrDefault(topic => topic.Id == id);
			if (topic == default(Topic)) 
			{
				return NotFound();
			}
			return View(new TopicViewModel
			{
				TopicTitle = topic.Title,
				TopicDescription = topic.Description,
				CourseName = SampleData.sampleCourses[1].Name,
				Assignments = topic.Tasks,
				Tests = topic.Tests,
			});
		}
	}
}
