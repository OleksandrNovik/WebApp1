using System.ComponentModel.DataAnnotations.Schema;

namespace BLL.Person_entities.UserFolder
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }

        [ForeignKey("UsersInfo")]
        public int InfoId { get; set; }
        public UserInfo Info { get; set; }

        [ForeignKey("Students")]
        public int? StudentId { get; set; }
        public Student? Student { get; set; }

        [ForeignKey("Mentors")]
        public int? MentorId { get; set; }
        public Mentor? Mentor { get; set; }

        [ForeignKey("UserPhotos")]
        public int? PhotoId { get; set; }
        public UserPhoto? UserPhoto { get; set; }
    }
}
