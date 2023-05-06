using BLL.Person_entities;

namespace BLL.Educational_entities.Organization
{
	public class Courses
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string? Description { get; set; }
		public decimal Rating { get; set; } = 0;
		public Mentor Author { get; set; }
		public Group? Group { get; set; }
	}
}
