using System.ComponentModel.DataAnnotations.Schema;

namespace BLL.Educational_entities.Education
{
	public class Test
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string? Description { get; set; }
		public Questions? Questions { get; set; }
		public ICollection<StudentAnswer>? StudentAnswers { get; set; }

		[ForeignKey("Topics")]
		public int TopicId { get; set; }
		public Topic OnTopic { get; set; }
		public AssignmentType Type { get; set; } = AssignmentType.Test;
        public string? LinkForInfo { get; set; }
    }
}
