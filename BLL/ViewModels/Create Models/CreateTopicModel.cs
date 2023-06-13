using System.ComponentModel.DataAnnotations;

namespace BLL.ViewModels.Create_Models
{
	public class CreateTopicModel
	{
		public int CourseId { get; set; }

		[Required (ErrorMessage = "Назва теми є обов'язковою")]
		public string Title { get; set; }
		
		[UIHint("MultilineText")]
		public string? Description { get; set; }

	}
}
