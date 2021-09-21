using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Models;
using Blog.Models.Entities;

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

        // GET: BlogController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Blog/Create
        //[Authorize]
        [HttpGet]
        public ActionResult Create()
        {
            //var newBlog = blogRepository.GetBlogEditViewModel();
            //return View(newBlog);
            return View();
        }

        // POST: BlogController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Models.Entities.Blog newBlog)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    blogRepository.Save(newBlog);
                    //TempData["message"] = string.Format("{0} har blitt opprettet", newBlog.Name);
                    return RedirectToAction(nameof(Index));
                }
                
                return View(newBlog);
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
