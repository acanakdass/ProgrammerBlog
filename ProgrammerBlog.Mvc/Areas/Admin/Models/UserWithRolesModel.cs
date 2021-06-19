using ProgrammerBlog.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgrammerBlog.Mvc.Areas.Admin.Models
{
    public class UserWithRolesModel
    {
        public User User { get; set; }
        public IList<string> Roles { get; set; }
    }
}
