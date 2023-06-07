namespace BLL.Educational_entities.Education.TestFolder.TestTypes
{
	public class OneAnswerTest
	{
		public int Id { get; set; }
		public string Question { get; set; }
		public Answers Correct { get; set; }
		public ICollection<Answers> Answers { get; set; }
		public Question OnQuestion { get; set; }
	}
}
