namespace BLL.Educational_entities.Education
{
	public class Question
	{
		public int Id { get; set; }
		public string ThisQuestion { get; set; }
		public ICollection<Answers> Answers { get; set; }
		public ICollection<Answers> CorrectAnswers { get; set; }
	}
}
