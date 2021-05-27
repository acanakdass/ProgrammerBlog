﻿using ProgrammerBlog.Shared.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammerBlog.Entities.Concrete
{
    public class User:IEntityRepository,IEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public string Username { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public ICollection<Article> Articles { get; set; }
    }
}