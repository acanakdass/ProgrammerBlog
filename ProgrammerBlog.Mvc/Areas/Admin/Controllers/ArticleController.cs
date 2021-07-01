using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProgrammerBlog.Mvc.Areas.Admin.Models;
using ProgrammerBlog.Services.Abstract;
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

        public ArticleController(IArticleService articleService, ICategoryService categoryService)
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
    }
}
