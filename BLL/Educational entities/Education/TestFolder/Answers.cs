namespace BLL.Educational_entities.Education
{
	public class Answers
	{
		public int Id { get; set; }
		public string Answer { get; set; }
		public Question Question { get; set; }
	}
}