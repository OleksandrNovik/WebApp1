using BLL.Educational_entities.Education.TestFolder.TestTypes;

namespace BLL.Educational_entities.Education
{
	public class Questions
	{
		public int Id { get; set; }
		public ICollection<FullAnswerTest>? FullAnswerTests { get; set; }
		public ICollection<OneAnswerTest>? OneAnswerTests { get; set; }
		public ICollection<ManyAnswerTest>? ManyAnswerTests { get; set; }
	}
}
