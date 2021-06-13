using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProgrammerBlog.Entities.Concrete;
using ProgrammerBlog.Entities.Dto;
using ProgrammerBlog.Mvc.Areas.Admin.Models;
using ProgrammerBlog.Services.Helper;
using ProgrammerBlog.Shared.Utilities.Extentions;
using ProgrammerBlog.Shared.Utilities.Helpers;
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
                userAddDto.Image = await ImageOps.ImageUpload(userAddDto.UserName,userAddDto.ImageFile); //ımage upload'dan dönen filename'i image'e atar.
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


        //USER DELETE
        public async Task<IActionResult> Delete(int userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                var deletedUser = JsonSerializer.Serialize(new UserDto
                {
                    User=user,
                    Message=$"{user.UserName} adlı kullanıcı başarıyla silindi",
                    ResultStatus=ResultStatus.Success
                });
                return Json(deletedUser);
            }
            else
            {
                string errorMessages ="";
                foreach (var error in result.Errors)
                {
                    errorMessages+=$"{error.Description} \n";
                }
                var deletedUserError = JsonSerializer.Serialize(new UserDto
                {
                    User = user,
                    Message = errorMessages,
                    ResultStatus = ResultStatus.Error
                });
                return Json(deletedUserError);
            }
        }

        //USER UPDATE
        [HttpGet]
        public async Task<PartialViewResult> Update(int userId)
        {
            //var user = await _userManager.FindByIdAsync(userId);
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
            var userUpdateDto = _mapper.Map<UserUpdateDto>(user);
            return PartialView("_UserUpdatePartial", userUpdateDto);
        }

        [HttpPost]
        public async Task<JsonResult> Update(UserUpdateDto userUpdateDto)
        {
            if (ModelState.IsValid)
            {
                var oldUser = await _userManager.FindByIdAsync(userUpdateDto.Id.ToString());
                var oldUserImage = oldUser.Image;
                bool isNewImageUploadSuceeded = false;
                if (userUpdateDto.ImageFile !=null)
                {
                    userUpdateDto.Image = await ImageOps.ImageUpload(userUpdateDto.UserName, userUpdateDto.ImageFile);
                    isNewImageUploadSuceeded = true;
                }
                var updatedUser = _mapper.Map<UserUpdateDto, User>(userUpdateDto, oldUser);  //userUpdateDto ve oldUser' harmanla ve updatedUser döndür
                var result = await _userManager.UpdateAsync(updatedUser);
                
                if (result.Succeeded)
                {
                    if (isNewImageUploadSuceeded)
                    {
                        Console.WriteLine("test");
                        ImageOps.DeleteImage(oldUserImage);
                    }
                    var userUpdateAjaxViewModel = JsonSerializer.Serialize(new UserUpdateAjaxViewModel
                    {
                        UserDto=new UserDto
                        {
                            Message=$"{updatedUser.UserName} adlı kullanıcı başarıyla güncellendi",
                            ResultStatus= ResultStatus.Success,
                            User= updatedUser
                        },
                        UserUpdatePartial = await this.RenderViewToStringAsync("_UserUpdatePartial", userUpdateDto),
                    });
                    Console.WriteLine("breakpoşnt");
                    return Json(userUpdateAjaxViewModel);
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }

                    var userUpdateErrorAjaxViewModel = JsonSerializer.Serialize(new UserUpdateAjaxViewModel
                    {
                        UserUpdateDto=userUpdateDto,
                        UserUpdatePartial = await this.RenderViewToStringAsync("_UserUpdatePartial", userUpdateDto),
                    });
                    Console.WriteLine("breakpoşnt error");

                    return Json(userUpdateErrorAjaxViewModel);

                }

            }
            var userUpdateErrorModelStateAjaxViewModel = JsonSerializer.Serialize(new UserUpdateAjaxViewModel
            {
                UserUpdateDto = userUpdateDto,
                UserUpdatePartial = await this.RenderViewToStringAsync("_UserUpdatePartial", userUpdateDto),
            });
            Console.WriteLine("breakpoşnt error");

            return Json(userUpdateErrorModelStateAjaxViewModel);
        }




        

    }
}
