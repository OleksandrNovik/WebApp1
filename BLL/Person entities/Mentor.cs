using BLL.Educational_entities.Organization;
using BLL.Person_entities.UserFolder;

namespace BLL.Person_entities
{
	public class Mentor
	{
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string? About{ get; set; }
		public ICollection<Courses>? Courses { get; set; }
		public User User { get; set; }
	}
}
