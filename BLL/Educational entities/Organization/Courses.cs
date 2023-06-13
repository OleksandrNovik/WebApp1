using BLL.Educational_entities.Education;
using BLL.Person_entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BLL.Educational_entities.Organization
{
	public class Courses
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string? Description { get; set; }
		public int AuthorId { get; set; }
		public Mentor Author { get; set; }
		public int GroupId { get; set; }
		public Group Group { get; set; }
		public DateTime CreationData { get; set; }
		public ICollection<Topic>? Topics { get; set; }
		public int OptionsId { get; set; }
		public CourseOptions Options { get; set; }
    }
}
