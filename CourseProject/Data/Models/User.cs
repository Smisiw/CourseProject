﻿using CourseProject.Data.Enum;

namespace CourseProject.Data.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } = "User";
    }
}
