using BLL.Person_entities;

namespace BLL.Other_models.UserFolder
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public UserInfo Info { get; set; }
        public Student? Student { get; set; }
        public Mentor? Mentor { get; set; }
    }
}
