using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProgrammerBlog.Entities.Concrete;
using ProgrammerBlog.Mvc.Areas.Admin.Models;
using ProgrammerBlog.Mvc.Models;
using ProgrammerBlog.Services.Abstract;
using ProgrammerBlog.Shared.Utilities.Results.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ProgrammerBlog.Mvc.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize(Roles = "Admin,Editor")]
    public class HomeController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly ICommentService _commentService;
        private readonly UserManager<User> _userManager;
        private readonly IArticleService _articleService;

        public HomeController(ICategoryService categoryService, ICommentService commentService, UserManager<User> userManager, IArticleService articleService)
        {
            _categoryService = categoryService;
            _commentService = commentService;
            _userManager = userManager;
            _articleService = articleService;
        }

        public async Task<IActionResult> Index()
        {
            var categoriesCountResult = await _categoryService.CountNonDeleteds();
            var articlesCountResult = await _articleService.CountNonDeleteds();
            var commentsCountResult = await _commentService.CountNonDeleteds();
            var usersCount = await _userManager.Users.CountAsync();
            var articlesResult = await _articleService.GetAll();

            if(categoriesCountResult.ResultStatus!=ResultStatus.Error && 
                commentsCountResult.ResultStatus!=ResultStatus.Error && 
                usersCount>-1 && articlesCountResult.ResultStatus!=ResultStatus.Error && articlesResult.ResultStatus != ResultStatus.Error)
            {
                var dashboardviewModel = new DashboardViewModel
                {
                    Articles = articlesResult.Data,
                    CategoriesCount = categoriesCountResult.Data,
                    ArticlesCount = articlesCountResult.Data,
                    UsersCount = usersCount,
                    CommentsCount = commentsCountResult.Data
                };
                return View(dashboardviewModel);
            }
            else
            {
                return NotFound();
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }
 
    }
}
