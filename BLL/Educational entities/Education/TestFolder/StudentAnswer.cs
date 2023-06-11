using BLL.Person_entities.UserFolder;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BLL.Educational_entities.Education
{
	public class StudentAnswer
	{
		public int Id { get; set; }
		public string CorrectAnswers { get; set; }

		[NotMapped]
		public StringBuilder CorrectAnswerBuilder { get; set; } = new StringBuilder("");
		public string WrongAnswers { get; set; }

		[NotMapped]
		public StringBuilder WrongAnswersBuidler { get; set; } = new StringBuilder("");
		public Mark? Mark { get; set; }

		[ForeignKey("User")]
		public int UserId { get; set; }
		public User Student { get; set; }
		public Test OnTest { get; set; }
		public DateTime PassDate { get; set; }
	}
}
