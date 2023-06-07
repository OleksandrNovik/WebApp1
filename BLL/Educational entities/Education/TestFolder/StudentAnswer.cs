using BLL.Person_entities.UserFolder;
using System.Text;

namespace BLL.Educational_entities.Education
{
	public class StudentAnswer
	{
		public string CorrectAnswers { get; set; }

		// Зробити, щоб мапер ігнорував
		public StringBuilder CorrectAnswerBuilder { get; set; } = new StringBuilder("");
		public string WrongAnswers { get; set; }

		// Зробити, щоб мапер ігнорував
		public StringBuilder WrongAnswersBuidler { get; set; } = new StringBuilder("");
		public Mark? Mark { get; set; }
		public User Student { get; set; }
		public Test OnTest { get; set; }
	}
}
