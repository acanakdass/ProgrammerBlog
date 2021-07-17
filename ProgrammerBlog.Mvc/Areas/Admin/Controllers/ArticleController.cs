using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProgrammerBlog.Entities.ComplexTypes;
using ProgrammerBlog.Entities.Concrete;
using ProgrammerBlog.Entities.Dto;
using ProgrammerBlog.Mvc.Areas.Admin.Models;
using ProgrammerBlog.Services.Abstract;
using ProgrammerBlog.Services.Helpers.Abstract;
using ProgrammerBlog.Shared.Utilities.Results.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace ProgrammerBlog.Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Editor")]
    public class ArticleController : BaseController
    {

        private readonly IArticleService _articleService;
        private readonly ICategoryService _categoryService;

        public ArticleController(IArticleService articleService, ICategoryService categoryService, UserManager<User> userManager,
            IMapper mapper, IImageHelper imageHelper) : base(userManager, mapper, imageHelper)
        {
            _articleService = articleService;
            _categoryService = categoryService;
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
            var categoriesToSelect = await _categoryService.GetAllNonDeletedAndActive();
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
                var articleAddDto = Mapper.Map<ArticleAddDto>(articleAddViewModel);
                var imageResult = await ImageHelper.Upload(articleAddViewModel.Title, articleAddViewModel.ThumbnailFile, ImageType.Post);
                articleAddDto.Thumbnail = imageResult.Data.Fullname;
                var result = await _articleService.Add(articleAddDto, LoggedInUser.UserName, LoggedInUser.Id);
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

        [HttpGet]
        public async Task<IActionResult> Update(int articleId)
        {
            var articleResult = await _articleService.GetArticleUpdateDto(articleId);
            var categoriesResult = await _categoryService.GetAllNonDeletedAndActive();
            if (articleResult.ResultStatus == ResultStatus.Success && categoriesResult.ResultStatus == ResultStatus.Success)
            {
                var articleUpdateViewModel = Mapper.Map<ArticleUpdateViewModel>(articleResult.Data);
                articleUpdateViewModel.Categories = categoriesResult.Data.Categories;
                return View(articleUpdateViewModel);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Update(ArticleUpdateViewModel articleUpdateViewModel)
        {
            if (ModelState.IsValid)
            {
                bool isNewThumbnailUploaded = false;
                var oldThumbnail = articleUpdateViewModel.Thumbnail;
                if (articleUpdateViewModel.ThumbnailFile != null)
                {
                    var uploadedImageResult = await ImageHelper.Upload(articleUpdateViewModel.Title, articleUpdateViewModel.ThumbnailFile, ImageType.Post);

                    if (uploadedImageResult.ResultStatus == ResultStatus.Success)
                        articleUpdateViewModel.Thumbnail = uploadedImageResult.Data.Fullname;
                    else
                        articleUpdateViewModel.Thumbnail = "postImages/defaultThumbnail.jpg";

                    if (oldThumbnail != "postImages/defaultThumbnail.jpg")
                    {
                        isNewThumbnailUploaded = true;
                    }
                }
                var articleUpdateDto = Mapper.Map<ArticleUpdateDto>(articleUpdateViewModel);
                var result = await _articleService.Update(articleUpdateDto, LoggedInUser.UserName);

                if (result.ResultStatus == ResultStatus.Success)
                {
                    if (isNewThumbnailUploaded)
                    {
                        ImageHelper.Delete(oldThumbnail);
                    }
                    TempData.Add("SuccessMessage", result.Message);
                    return RedirectToAction("Index", "Article");
                }
                else
                {
                    ModelState.AddModelError("", result.Message);
                }

            }
            var categories = await _categoryService.GetAllNonDeletedAndActive();
            articleUpdateViewModel.Categories = categories.Data.Categories;
            return View(articleUpdateViewModel);
        }

        [HttpPost]
        public async Task<JsonResult> Delete(int articleId)
        {
            var result = await _articleService.Delete(articleId, LoggedInUser.UserName);
            var articleResult = JsonSerializer.Serialize(result);
            return Json(articleResult);
        }
    }
}
