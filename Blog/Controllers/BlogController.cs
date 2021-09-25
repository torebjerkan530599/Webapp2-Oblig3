using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Authorization;
using Blog.Models;
using Blog.Models.Entities;
using Blog.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace Blog.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogRepository _blogRepository;
        IAuthorizationService _authorizationService;

        public BlogController(IBlogRepository blogRepository, IAuthorizationService authorizationService = null)
        {
            _blogRepository = blogRepository;
            _authorizationService = authorizationService;
        }

        // GET: BlogController
        public ActionResult Index()
        {
         
            var blogs = _blogRepository.GetAllBlogs();//.ToList(); ;
            return View(blogs);
        }

        [HttpGet]
        public ActionResult ReadBlog(int id)
        {

            var blog = _blogRepository.GetBlog(id);
            var posts = _blogRepository.GetAllPosts(id);

            if (!ModelState.IsValid) return View();

            BloggViewModel bv = new BloggViewModel()
            {
                BlogId = id,
                Name = blog.Name,
                Title = (from p in posts where p.BlogId==id select p.Title).ToString(),
                Posts = posts.ToList()
            };
            
            return View(bv);
        }

        public ActionResult ReadPost(int id)
        {
            var postViewModel = _blogRepository.GetPostViewModel(id);
            return View(postViewModel);
        }


        // GET: Blogg/Create
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
            //var newBlog = new CreateBloggViewModel();//blogRepository.GetCreateBlogViewModel();
            //return View(newBlog);
            return View();
        }

        // POST: Blog/Create
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Name, ClosedForPosts, Created, Owner")]CreateBloggViewModel newBlog) //Bind owner også senere.
        {
            try
            {
                if (!ModelState.IsValid) return View();
                var blog = new Blogg()
                {
                    Name = newBlog.Name,
                    Created = newBlog.Created,
                    ClosedForPosts = newBlog.ClosedForPosts,
                    Owner = newBlog.Owner

                };
                    _blogRepository.SaveBlog(blog,User).Wait();
                    TempData["message"] = $"{blog.Name} har blitt opprettet";
                    return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [Authorize]
        [HttpGet]
        public ActionResult CreatePost()
        {
            return View();
        }

        // POST: Blog/Create
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePost(/*[Bind("Title, Content, Created, Modified, NumberOfComments")]*/Post newPost) //trenger å lagre Blogg tilhørighet, må jeg sende med id her?
        {
            try
            {
                if (!ModelState.IsValid) return View();
                
                _blogRepository.SavePost(newPost,User).Wait();
                TempData["message"] = $"{newPost.Title} har blitt opprettet";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [Authorize]
        [HttpGet]
        public ActionResult CreateComment()
        {
            //var newBlog = new CreateBloggViewModel();//blogRepository.GetCreateBlogViewModel();
            //return View(newBlog);
            return View();
        }

        // POST: Blog/Create
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult CreateComment(/*[Bind(...)]*/Comment newComment) //add Bind later
        {
            try
            {
                if (!ModelState.IsValid) return View();
                
                _blogRepository.SaveComment(newComment,User).Wait();
                TempData["message"] = $"{newComment.Text} har blitt opprettet";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Contact/Edit/#
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
         
            /*Post ppst = (from c in _db.Contact
                where c.ContactId == id
                select c).FirstOrDefault();*/

            var postEditViewModel = _blogRepository.GetBlog(id);//GetPostEditViewModel(id);

                
            // requires using ContactManager.Authorization;
            var isAuthorized = await _authorizationService.AuthorizeAsync(
                User, postEditViewModel, BlogOperations.Update);
            if (!isAuthorized.Succeeded)
            {
                return new ChallengeResult();
            }
            //return RedirectToAction(nameof(Index));

            return View(postEditViewModel);
        }


        // POST: Contact/Edit/#
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(/*[Bind(...")]*/int? id, Post post)
        {
            if(id == null)
            {
                return NotFound();
            }

            
            // requires using ContactManager.Authorization;
            var isAuthorized = await _authorizationService.AuthorizeAsync(
                User, post, BlogOperations.Update);
            if (!isAuthorized.Succeeded)
            {
                return View("Ingentilgang");
            }
            try 
            {
                if (ModelState.IsValid)
                {
                    _blogRepository.SavePost(post,User).Wait();
                    TempData["message"] = $"{post.Title} er oppdatert";
                    return RedirectToAction("Index");
                } else return new ChallengeResult();
            } catch {
                return View(post);
            }
        }



        // GET: BlogController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BlogController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
