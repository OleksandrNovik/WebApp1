using BLL.Educational_entities.Education;

namespace BLL.ViewModels
{
	public class TopicViewModel
	{
		public string CourseName { get; set; }
		public string TopicTitle { get; set; }
		public string? TopicDescription { get; set; }
		public IEnumerable<Assignment>? Assignments { get; set; }
		public IEnumerable<Test>? Tests { get; set; }
	}
}
