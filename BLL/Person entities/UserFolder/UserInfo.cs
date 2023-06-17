using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BLL.Person_entities.UserFolder
{
    public enum Theme
    {
        Light, Dark
    }
    public enum UserRole
    {
        Student,
        Mentor,
        Admin
    }
    public class UserInfo
    {
        [Key]
        [ForeignKey("Users")]
		public int UserId { get; set; }
		public string Email { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }
        public User OnUser { get; set; }
        public Theme Theme { get; set; } = Theme.Light;
        public string? EditorTheme { get; set; }
    }
}
