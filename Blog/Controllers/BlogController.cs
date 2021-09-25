﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Models;
using Blog.Models.Entities;
using Blog.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace Blog.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogRepository _blogRepository;

        public BlogController(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
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

        // GET: BlogController/Details/5
        public ActionResult Details(int id)
        {
            return View();
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
        public ActionResult Create([Bind("Name, ClosedForPosts, Created")]CreateBloggViewModel newBlog) //Bind owner også senere.
        {
            try
            {
                if (!ModelState.IsValid) return View();
                var blog = new Blogg()
                {
                    Name = newBlog.Name,
                    Created = newBlog.Created,
                    ClosedForPosts = newBlog.ClosedForPosts

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
