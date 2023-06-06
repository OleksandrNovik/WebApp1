using BLL.Person_entities;

namespace BLL.Educational_entities.Organization
{
	public class Group
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public Courses Course { get; set; }
		public ICollection<Student> Students { get; set; } = new List<Student>();
	}
}
