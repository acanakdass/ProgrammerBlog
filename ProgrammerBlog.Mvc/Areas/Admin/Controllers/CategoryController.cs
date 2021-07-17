using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProgrammerBlog.Entities.Concrete;
using ProgrammerBlog.Entities.Dto;
using ProgrammerBlog.Mvc.Areas.Admin.Models;
using ProgrammerBlog.Services.Abstract;
using ProgrammerBlog.Services.Helpers.Abstract;
using ProgrammerBlog.Shared.Utilities.Extentions;
using ProgrammerBlog.Shared.Utilities.Results.ComplexTypes;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ProgrammerBlog.Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Editor")]
    public class CategoryController : BaseController
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService, UserManager<User> userManager,
            IMapper mapper, IImageHelper imageHelper) : base(userManager, mapper, imageHelper)
        {
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _categoryService.GetAllNonDeleted();

            return View(result.Data);

        }



        /// ADD CATEGORY




        [HttpGet]
        public IActionResult Add()
        {
            return PartialView("_CategoryAddPartial");
        }
        [HttpPost]
        public async Task<IActionResult> Add(CategoryAddDto categoryAddDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _categoryService.Add(categoryAddDto,LoggedInUser.UserName);
                if (result.ResultStatus == ResultStatus.Success)
                {
                    //View'i jsona çevirir ve gönderir, daha sonra jquery ile render edilir
                    var categoryAddAjaxViewModel = JsonSerializer.Serialize(new CategoryAddAjaxViewModel
                    {
                        CategoryDto = result.Data,
                        CategoryAddPartial = await this.RenderViewToStringAsync("_CategoryAddPartial", categoryAddDto)
                    });

                    return Json(categoryAddAjaxViewModel);
                }
            }
            var categoryAddAjaxErrorModel = JsonSerializer.Serialize(new CategoryAddAjaxViewModel
            {
                CategoryAddPartial = await this.RenderViewToStringAsync("_CategoryAddPartial", categoryAddDto)
            });
            return Json(categoryAddAjaxErrorModel);
        }


        /// UPDATE CATEGORY


        [HttpGet]
        public async Task<IActionResult> Update(int categoryId)
        {
            var result = await _categoryService.GetCategoryUpdateDto(categoryId);

            if (result.ResultStatus == ResultStatus.Success)
            {
                return PartialView("_CategoryUpdatePartial", result.Data);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Update(CategoryUpdateDto categoryUpdateDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _categoryService.Update(categoryUpdateDto, LoggedInUser.UserName);
                if (result.ResultStatus == ResultStatus.Success)
                {
                    //View'i jsona çevirir ve gönderir, daha sonra jquery ile render edilir
                    var categoryUpdateAjaxViewModel = JsonSerializer.Serialize(new CategoryUpdateAjaxViewModel
                    {
                        CategoryDto = result.Data,
                        CategoryUpdatePartial = await this.RenderViewToStringAsync("_CategoryUpdatePartial", categoryUpdateDto)
                    });


                    return Json(categoryUpdateAjaxViewModel);
                }
            }
            var categoryUpdateAjaxErrorModel = JsonSerializer.Serialize(new CategoryUpdateAjaxViewModel
            {
                CategoryUpdatePartial = await this.RenderViewToStringAsync("_CategoryUpdatePartial", categoryUpdateDto)
            });
            return Json(categoryUpdateAjaxErrorModel);
        }



        public async Task<JsonResult> GetAllCategories()
        {
            var result = await _categoryService.GetAll();
            var categories = JsonSerializer.Serialize(result.Data, new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            });
            return Json(categories);
        }

        public async Task<JsonResult> GetNonDeletedCategories()
        {
            var result = await _categoryService.GetAllNonDeleted();
            var categories = JsonSerializer.Serialize(result.Data, new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            });
            return Json(categories);
        }

        [HttpPost]
        public async Task<JsonResult> Delete(int categoryId)
        {
            var result = await _categoryService.Delete(categoryId, LoggedInUser.UserName);
            var deletedCategory = JsonSerializer.Serialize(result.Data);
            return Json(deletedCategory);
        }

        [HttpGet]
        public async Task<JsonResult> GetCategory(int categoryId)
        {
            var result = await _categoryService.Get(categoryId);
            var category = JsonSerializer.Serialize(result.Data, new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            });
            return Json(category);
        }
    }
}

