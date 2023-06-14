using BLL.Person_entities;
using BLL.Person_entities.UserFolder;
using System.ComponentModel.DataAnnotations.Schema;

namespace BLL.Educational_entities.Education
{
	public enum WorkStatus
	{
		NotStarted,
		NotComplited,
		Complited,
		Evaluated
	}
	public class Work
	{
		public int Id { get; set; }
		public WorkStatus Status { get; set; } = WorkStatus.NotStarted;
		public Assignment OnTask { get; set; }

		[ForeignKey("Users")]
		public int UserId { get; set; }
		public Student WorkAuthor { get; set; }
		public Mark? Mark { get; set; }
		public string? Code { get; set; }
		public string? ProgramingLanguage { get; set; }
		public DateTime SubmitDate { get; set; }
	}
}
