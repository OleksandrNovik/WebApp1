using BLL.Person_entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace BLL.Educational_entities.Organization
{
	public class Group
	{
		public int Id { get; set; }
		public string Name { get; set; }

		[ForeignKey("Courses")]
		public int CourseId { get; set; }
		public Courses Course { get; set; }

		public ICollection<Student>? Students { get; set; }
	}
}
