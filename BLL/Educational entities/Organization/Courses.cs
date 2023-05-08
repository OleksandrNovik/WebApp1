using BLL.Educational_entities.Education;
using BLL.Person_entities;

namespace BLL.Educational_entities.Organization
{
	public class Courses
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string? Description { get; set; }
		public Mentor Author { get; set; }
		public Group? Group { get; set; }
		public DateTime CreationData { get; set; }
		public ICollection<Topic>? Topics { get; set; }
	}
}
