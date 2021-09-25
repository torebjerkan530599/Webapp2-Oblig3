using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Models;
using Blog.Models.Entities;
using Blog.Models.ViewModels;

namespace Blog.Controllers
{
    public class BlogController : Controller
    {
        private IBlogRepository blogRepository;

        public BlogController(IBlogRepository _blogRepository)
        {
            blogRepository = _blogRepository;
        }

        // GET: BlogController
        public ActionResult Index()
        {
         
            var blogs = blogRepository.GetAllBlogs();//.ToList(); ;
            return View(blogs);
        }

        [HttpGet]
        public ActionResult ReadBlog(int id)
        {

            var blog = blogRepository.GetBlog(id);
            var posts = blogRepository.GetAllPosts(id);

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

        // GET: BlogController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Blogg/Create
        //[Authorize]
        [HttpGet]
        public ActionResult Create()
        {
            var newBlog = blogRepository.GetCreateBlogViewModel();
            //return View(newBlog);
            return View(newBlog);
        }

        // POST: Blog/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Name")]Blogg newBlog)
        {
            try
            {
                if (!ModelState.IsValid) return View();
                var p = new Blogg()
                {
                    Name = newBlog.Name,
                    Created = DateTime.Now
                    
                    //Description = newBlog.Description,
                    
                };


                    blogRepository.Save(newBlog);
                    //TempData["message"] = string.Format("{0} har blitt opprettet", newBlog.Name);
                    return RedirectToAction(nameof(Index));
                    
            }

            catch
            {
                return View();
            }
        }

        // GET: BlogController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: BlogController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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
