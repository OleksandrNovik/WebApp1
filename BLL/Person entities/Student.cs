using BLL.Educational_entities.Education;
using BLL.Educational_entities.Organization;
using BLL.Person_entities.UserFolder;

namespace BLL.Person_entities
{
	public class Student
	{
		public int StudentId { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public ICollection<Group>? Groups { get; set; }
		public ICollection<Mark>? Marks { get; set; }
		public User User { get; set; }
	}
}
