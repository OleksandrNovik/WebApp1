namespace BLL.Educational_entities.Education.TestFolder.TestTypes
{
	public class ManyAnswerTest
	{
		public int Id { get; set; }
		public string Question { get; set; }
		public ICollection<Answers> Answers { get; set; }
		public Questions OnQuestion { get; set; }
	}
}
