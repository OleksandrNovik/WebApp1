namespace BLL.Educational_entities.Organization
{
	public enum CourseLevel
	{
		Beginner,
		Intermediate,
		Advanced
	}
	public enum CourseType
	{
		Desktop,
		Web,
		Mobile,
		Gamedev,
		Database	
	}
	public class CourseOptions
	{
		public int Id { get; set; }
		public Courses Course { get; set; }
		public bool IsPublic { get; set; }
		public CourseLevel Level { get; set; }
		public CourseType Type { get; set; }
		public ICollection<CourseAdditionalCharacteristics>? AdditionalInfo { get; set; }
	}
}
