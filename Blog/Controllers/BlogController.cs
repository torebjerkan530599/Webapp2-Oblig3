using Blog.Models;
using Blog.Models.Entities;
using Blog.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogRepository _blogRepository;
        readonly IAuthorizationService _authorizationService;
        //private readonly UserManager<IdentityUser> _userManager;

        public BlogController(IBlogRepository blogRepository, IAuthorizationService authorizationService = null)
        {
            _blogRepository = blogRepository;
            _authorizationService = authorizationService;
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Privacy()
        {
            return View();
        }

        [AllowAnonymous]
        // GET: Blog
        public async Task<ActionResult> Index()
        {

            var blogs = await _blogRepository.GetAllBlogs();//.ToList(); ;
            return View(blogs);
        }

        // GET: Blog/ReadBlog/5
        [AllowAnonymous]
        [HttpGet]
        public ActionResult ReadBlog(int id) //shows all posts on blog
        {

            var blog = _blogRepository.GetBlog(id);
            var posts = _blogRepository.GetAllPosts(id);
            var tagsForThisBlog = _blogRepository.GetAllTagsForBlog(blog.BlogId).ToList();

            if (!ModelState.IsValid) return View();

            var bv = new BloggViewModel
            {
                BlogId = id,
                Name = blog.Name,
                Title = (from p in posts where p.BlogId == id select p.Title).ToString(),
                Posts = posts.ToList(), //hvorfor "possible multiple enumeration"?
                Owner = blog.Owner, //another way of including owner
                Tags = tagsForThisBlog
            };

            return View(bv);
        }


        // GET: Blogg/Create
        //[Authorize]
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Blogg/Create
        [HttpPost]
        //[Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Name, ClosedForPosts, Created, Owner")] CreateBloggViewModel newBlog)
        {
            try
            {
                if (!ModelState.IsValid) return View();
                var blog = new Blogg()
                {
                    Name = newBlog.Name,
                    Created = DateTime.Now,
                    ClosedForPosts = newBlog.ClosedForPosts,
                    Owner = newBlog.Owner

                };
                _blogRepository.SaveBlog(blog, User).Wait();
                TempData["message"] = $"{blog.Name} har blitt opprettet";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
