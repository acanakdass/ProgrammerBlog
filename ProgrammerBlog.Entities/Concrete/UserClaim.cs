﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammerBlog.Entities.Concrete
{
    public class UserClaim : IdentityUserClaim<int>  //int: primary key tipi
    {
    }
}
