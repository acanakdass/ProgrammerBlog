using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProgrammerBlog.Entities.Concrete;
using ProgrammerBlog.Services.Helpers.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgrammerBlog.Mvc.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        protected UserManager<User> UserManager { get; }
        protected IMapper Mapper { get; }
        protected IImageHelper ImageHelper { get; }
        protected User LoggedInUser => UserManager.GetUserAsync(HttpContext.User).Result;

        public BaseController(UserManager<User> userManager, IMapper mapper, IImageHelper ımageHelper)
        {
            UserManager = userManager;
            Mapper = mapper;
            ImageHelper = ımageHelper;
        }
    }
}
