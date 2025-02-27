﻿namespace UserService.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}
