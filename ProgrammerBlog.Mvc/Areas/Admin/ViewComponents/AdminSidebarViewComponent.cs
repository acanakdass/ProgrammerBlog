using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using ProgrammerBlog.Entities.Concrete;
using ProgrammerBlog.Mvc.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgrammerBlog.Mvc.Areas.Admin.ViewComponents
{
    public class AdminSidebarViewComponent : ViewComponent
    {
        private readonly UserManager<User> _userManager;

        public AdminSidebarViewComponent(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public ViewViewComponentResult Invoke()
        {
            var user = _userManager.GetUserAsync(HttpContext.User).Result; //anlık giriş yapmış kullanıcıyı getir
            var roles = _userManager.GetRolesAsync(user).Result; //user'a ait rolleri verir

            return View(new UserWithRolesModel
            {
                User = user,
                Roles = roles
            });
        }
    }
}
