using BLL.Educational_entities.Organization;

namespace BLL.Person_entities
{
	public class Student
	{
		public int StudentId { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public ICollection<Group> Groups { get; set; } = new List<Group>();
	}
}
