using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProgrammerBlog.Entities.ComplexTypes;
using ProgrammerBlog.Entities.Dto;
using ProgrammerBlog.Mvc.Areas.Admin.Models;
using ProgrammerBlog.Services.Abstract;
using ProgrammerBlog.Services.Helpers.Abstract;
using ProgrammerBlog.Shared.Utilities.Results.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgrammerBlog.Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Editor")]
    public class ArticleController : Controller
    {

        private readonly IArticleService _articleService;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        private readonly IImageHelper _imageHelper;

        public ArticleController(IArticleService articleService, ICategoryService categoryService, IMapper mapper, IImageHelper imageHelper)
        {
            _articleService = articleService;
            _categoryService = categoryService;
            _mapper = mapper;
            _imageHelper = imageHelper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await _articleService.GetAllNonDeleted();
            if (result.ResultStatus == ResultStatus.Success)
                return View(result.Data);
            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var categoriesToSelect = await _categoryService.GetAllNonDeleted();
            if (categoriesToSelect.ResultStatus == ResultStatus.Success)
            {
                return View(new ArticleAddViewModel
                {
                    Categories = categoriesToSelect.Data.Categories
                });
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Add(ArticleAddViewModel articleAddViewModel)
        {
            var categories = await _categoryService.GetAllNonDeleted();
            articleAddViewModel.Categories = categories.Data.Categories;
            if (ModelState.IsValid)
            {
                var articleAddDto = _mapper.Map<ArticleAddDto>(articleAddViewModel);
                var imageResult = await _imageHelper.Upload(articleAddViewModel.Title, articleAddViewModel.ThumbnailFile, ImageType.Post);
                articleAddDto.Thumbnail = imageResult.Data.Fullname;
                var result = await _articleService.Add(articleAddDto, "Ahmet Can");
                if (result.ResultStatus == ResultStatus.Success)
                {
                    TempData.Add("SuccessMessage", result.Message);
                    return RedirectToAction("Index", "Article");
                }
                else
                {
                    ModelState.AddModelError("", result.Message);
                    return View(articleAddViewModel);
                }
            }
            return View(articleAddViewModel);
            
        }
    }
}
