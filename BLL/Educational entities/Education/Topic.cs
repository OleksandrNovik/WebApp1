using BLL.Educational_entities.Organization;

namespace BLL.Educational_entities.Education
{
	public class Topic
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string? Description { get; set; }
		public Courses Course { get; set; }
		public ICollection<Test>? Tests { get; set; }
	}
}
