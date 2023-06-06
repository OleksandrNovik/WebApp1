namespace BLL.Educational_entities.Education
{
	public class Test
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string? Description { get; set; }
		public ICollection<Question>? Questions { get; set; }
		public ICollection<StudentAnswer>? StudentAnswers { get; set; }
		public AssignmentType Type { get; set; } = AssignmentType.Test;
	}
}
