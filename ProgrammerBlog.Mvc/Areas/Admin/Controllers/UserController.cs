using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProgrammerBlog.Entities.Concrete;
using ProgrammerBlog.Entities.Dto;
using ProgrammerBlog.Shared.Utilities.Extentions;
using ProgrammerBlog.Shared.Utilities.Results.ComplexTypes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ProgrammerBlog.Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {

        private readonly UserManager<User> _userManager;

        public UserController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();

            var usersDto = new UserListDto
            {
                Users = users,
                Message = "Kullanıcılar veritabanından başarıyla çekildi",
                ResultStatus = ResultStatus.Success
            };

            return View(usersDto);
        }
        [HttpGet]
        public IActionResult Add()
        {
            return PartialView("_UserAddPartial");
        }
        

        public async Task<string> ImageUpload(UserAddDto userAddDto)
        {
            //string fileName = Path.GetFileNameWithoutExtension(userAddDto.Image.FileName);  //file ismini uzantısı .jpg vs olmadan alır
            string fileExtention = Path.GetExtension(userAddDto.Image.FileName);
            DateTime datetime = DateTime.Now;
            string fileName = $"{userAddDto.UserName}_{datetime.FullDateAndTimeStringWithUnderscore()}{fileExtention}";
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\userImages", fileName);

            await using (var stream = new FileStream(path,FileMode.Create))
            {
                await userAddDto.Image.CopyToAsync(stream);
            }

            return fileName;

        }
        
    }
}
