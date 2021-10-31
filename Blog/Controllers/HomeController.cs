using Blog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Blog.Models.Entities;
using Blog.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Blog.Controllers
{
    public class HomeController : Controller
    {

        private readonly IBlogRepository _blogRepository;
        private UserManager<ApplicationUser> userManager;

        public HomeController(IBlogRepository repository, UserManager<ApplicationUser> userManager1 = null, IAuthorizationService authorizationService1 = null)
        {
            _blogRepository = repository;
            userManager = userManager1;
        }

        public IActionResult Index()
        {
            var user = userManager.GetUserAsync(User).Result;

            var blogs = _blogRepository.GetAllSubscribedBlogs(user);
            var posts = _blogRepository.GetLatestUpdatesOnPosts();
            var tags = _blogRepository.GetAllTags().Result;
            var comments =  _blogRepository.GetAllComments().Result;

            IndexViewModel indexViewModel = new IndexViewModel()
            {
                Blogs = blogs,
                Posts = posts,
                Tags = tags,
                Comments = comments
            };

            return View(indexViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
