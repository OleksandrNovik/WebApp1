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
		Programming,
		Language,
		Historical,
		Geographical,
		Calculus
	}
	public class CourseOptions
	{
		public int Id { get; set; }
		public Courses Course { get; set; }
		public bool IsPublic { get; set; }
		public CourseLevel Level { get; set; }
		public CourseType Type { get; set; }
		//TODO: Змінити формат
		public string[]? AdditionalInfo { get; set; }
	}
}
