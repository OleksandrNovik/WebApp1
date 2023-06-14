using BLL.Educational_entities.Education;
using BLL.Educational_entities.Organization;
using System.ComponentModel.DataAnnotations;

namespace BLL.ViewModels.Create_Models
{
	public class CreateTopicModel
	{
		public int CourseId { get; set; }
		public int TopicId { get; set; }

		[Required (ErrorMessage = "Назва теми є обов'язковою")]
		public string Title { get; set; }
		
		[UIHint("MultilineText")]
		public string? Description { get; set; }


		public string? CourseName { get; set; }
		public ICollection<Assignment>? Assignments { get; set; }
		public ICollection<Test>? Tests { get; set; }

	}
}
