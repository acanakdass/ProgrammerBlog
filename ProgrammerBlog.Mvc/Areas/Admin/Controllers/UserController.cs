using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProgrammerBlog.Entities.Concrete;
using ProgrammerBlog.Entities.Dto;
using ProgrammerBlog.Mvc.Areas.Admin.Models;
using ProgrammerBlog.Shared.Utilities.Extentions;
using ProgrammerBlog.Shared.Utilities.Results.ComplexTypes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ProgrammerBlog.Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {

        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public UserController(UserManager<User> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
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
        public async Task<JsonResult> GetAllUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            var userListDto = JsonSerializer.Serialize(new UserListDto
            {
                Users=users,
                ResultStatus=ResultStatus.Success
            }, new JsonSerializerOptions
            { 
                ReferenceHandler = ReferenceHandler.Preserve
            });
            return Json(userListDto);
        }


        //User ADD


        [HttpGet]
        public IActionResult Add()
        {
            return PartialView("_UserAddPartial");
        }
        [HttpPost]
        public async Task<IActionResult> Add(UserAddDto userAddDto)
        {
            if (ModelState.IsValid)
            {
                userAddDto.Image = await ImageUpload(userAddDto); //ımage upload'dan dönen filename'i image'e atar.
                var user = _mapper.Map<User>(userAddDto); //userAddDto'yu User nesenesine çevir.
                var result = await _userManager.CreateAsync(user, userAddDto.Password);
                if (result.Succeeded)
                {
                    var userAddAjaxViewModel = JsonSerializer.Serialize(new UserAddAjaxViewModel
                    {
                        UserDto = new UserDto
                        {
                            ResultStatus = ResultStatus.Success,
                            Message = $"{user.UserName} adlı kullanıcı başarıyla eklendi",
                            User = user
                        },
                        UserAddPartial = await this.RenderViewToStringAsync("_UserAddPartial", userAddDto)
                    });
                    return Json(userAddAjaxViewModel);
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    var userAddAjaxErrorModel = JsonSerializer.Serialize(new UserAddAjaxViewModel
                    {
                        UserAddDto = userAddDto,
                        UserAddPartial = await this.RenderViewToStringAsync("_UserAddPartial", userAddDto)
                    });
                    return Json(userAddAjaxErrorModel);

                }
            }
            var userAddAjaxModelStateErrorModel = JsonSerializer.Serialize(new UserAddAjaxViewModel
            {
                UserAddDto = userAddDto,
                UserAddPartial = await this.RenderViewToStringAsync("_UserAddPartial", userAddDto)
            });
            return Json(userAddAjaxModelStateErrorModel);
        }


        //User ADD Ends




        public async Task<string> ImageUpload(UserAddDto userAddDto)
        {
            //string fileName = Path.GetFileNameWithoutExtension(userAddDto.ImageFile.FileName);  //file ismini uzantısı .jpg vs olmadan alır
            string fileExtention = Path.GetExtension(userAddDto.ImageFile.FileName);
            DateTime datetime = DateTime.Now;
            string fileName = $"{userAddDto.UserName}_{datetime.FullDateAndTimeStringWithUnderscore()}{fileExtention}";
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\userImages", fileName);

            await using (var stream = new FileStream(path, FileMode.Create))
            {
                await userAddDto.ImageFile.CopyToAsync(stream);
            }

            return fileName;

        }

    }
}
