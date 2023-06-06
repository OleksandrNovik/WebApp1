using BLL.Educational_entities.Organization;
using BLL.Person_entities;
using BLL.Person_entities.UserFolder;

namespace BLL.ViewModels
{
	public class UserViewModel
	{
		public User ShownUser { get; set; }
		public Mentor? UserMentor { get; set; } = null;
		public IEnumerable<Courses>? CommonCourses { get; set; } = null;
		public bool EditProfile { get; set; } = false;
	}
}
