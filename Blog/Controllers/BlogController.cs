using Blog.Models;
using Blog.Models.Entities;
using Blog.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
//using Microsoft.AspNetCore.SignalR;
//using Microsoft.AspNetCore.SignalR.Client;

namespace Blog.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogRepository _blogRepository;
        readonly IAuthorizationService _authorizationService;
        //private readonly UserManager<ApplicationUser> _userManager;
        //HubConnection connection;

        public BlogController(IBlogRepository blogRepository, IAuthorizationService authorizationService = null)
        {
            _blogRepository = blogRepository;
            _authorizationService = authorizationService;
            /*connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:44462/SignalRHub")
                .Build();

            connection.Closed += async (error) =>
            {
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await connection.StartAsync();
            };

            connection.On<string, string>("ReceiveNewPost", (user, message) =>
            {
                this.Dispatcher.Invoke(() =>
                {
                    var newMessage = $"{user}: {message}";
                    messagesList.Items.Add(newMessage);
                });
            });

            try
            {
                await connection.StartAsync();
                messagesList.Items.Add("Connection started");
                connectButton.IsEnabled = false;
                sendButton.IsEnabled = true;
            }
            catch (Exception ex)
            {
                messagesList.Items.Add(ex.Message);
            }*/
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
