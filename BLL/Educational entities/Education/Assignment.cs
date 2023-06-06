namespace BLL.Educational_entities.Education
{
	public enum AssignmentType
	{
		Code, Test
	}
	public class Assignment
    {
		public int Id { get; set; }
		public string Name { get; set; }
		public string? Description { get; set; }
		public string MainTask { get; set; }
		public string? StarterCode { get; set; }
		public ICollection<Work>? StudentWorks { get; set; }
		public AssignmentType Type { get; set; } = AssignmentType.Code;
		public string? ProgramingLanguage { get; set; }
	}
}
