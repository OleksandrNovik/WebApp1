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
		public Question Question { get; set; }
		public AnswerStatus IsCorrect { get; set; } = AnswerStatus.Wrong; 
	}
}