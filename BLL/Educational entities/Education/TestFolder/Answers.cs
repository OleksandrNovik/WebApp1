using BLL.Educational_entities.Education.TestFolder.TestTypes;
namespace BLL.Educational_entities.Education
{
	public enum AnswerStatus
	{
		Correct, Wrong
	}
	public class Answers
	{
		public int Id { get; set; }
		public string Answer { get; set; }
		public Questions Question { get; set; }
		public AnswerStatus IsCorrect { get; set; } = AnswerStatus.Wrong; 
		public OneAnswerTest? OneAnswerTest { get; set; }
		public	ManyAnswerTest? ManyAnswerTest { get; set; }
	}
}