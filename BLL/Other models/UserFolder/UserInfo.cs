﻿namespace BLL.Other_models.UserFolder
{
    public enum UserRole
    {
        Student,
        Mentor,
        Admin
    }
    public class UserInfo
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }
    }
}
