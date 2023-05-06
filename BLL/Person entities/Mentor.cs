using BLL.Educational_entities.Organization;

namespace BLL.Person_entities
{
	public class Mentor
	{
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public decimal Rating { get; set; } = 0;
		public ICollection<Courses>? Courses { get; set; }
	}
}
