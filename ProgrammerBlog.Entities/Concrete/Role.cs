using Microsoft.AspNetCore.Identity;
using ProgrammerBlog.Shared.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammerBlog.Entities.Concrete
{
    public class Role:IdentityRole<int>  //int : primary key için kullanılacak tip
    {
        
    }
}
