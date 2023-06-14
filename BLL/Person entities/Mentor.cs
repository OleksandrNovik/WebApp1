using BLL.Educational_entities.Organization;
using BLL.Person_entities.UserFolder;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BLL.Person_entities
{
	public class Mentor
	{
        public int Id { get; set; }
        public string? About{ get; set; }
		public ICollection<Courses>? Courses { get; set; }
        
        [ForeignKey("Users")]
        public int UserId { get; set; }
        public User User { get; set; }
	}
}
