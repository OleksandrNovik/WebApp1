using BLL.Educational_entities.Education;
using BLL.Educational_entities.Organization;
using BLL.Person_entities.UserFolder;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BLL.Person_entities
{
	public class Student
	{
		public int Id { get; set; }
        
		[ForeignKey("Users")]
        public int UserId { get; set; }
        public ICollection<Group>? Groups { get; set; }
		public ICollection<Mark>? Marks { get; set; }
		public User User { get; set; }
	}
}
