using Blog.Authorization;
using Blog.Models;
using Blog.Models.Entities;
using Blog.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Controllers
{
    public class PostController : Controller
    {
        private readonly IBlogRepository _blogRepository;
        readonly IAuthorizationService _authorizationService;

        public PostController(IBlogRepository blogRepository, IAuthorizationService authorizationService = null)
        {
            _blogRepository = blogRepository;
            _authorizationService = authorizationService;
        }


        [AllowAnonymous]
        public ActionResult ReadPost(int id) //show all comments on post
        {
            var postViewModel = _blogRepository.GetPostViewModel(id);
            TempData["chosenId"] = id; //transfer the id to view for use by javascript

            return View(postViewModel);
        }


        [HttpGet]
        public async Task<ActionResult> CreatePost(int blogId)
        {
            var blog = _blogRepository.GetBlog(blogId);


            // requires using AuthorizationHandler
            var isAuthorized = await _authorizationService.AuthorizeAsync(
                User, blog, BlogOperations.Update);

            if (!isAuthorized.Succeeded)
                return View("Ingentilgang");


            var closed = blog.ClosedForPosts;

            if (closed) //hvis ikke stengt, gå til opprett post
            {
                //hvis bloggen er stengt, gi beskjed.
                TempData["message"] = "Bloggen er stengt for innlegg";
                return RedirectToAction("ReadBlog", "Blog", new { id = blogId });
            }

            List<Tag> tags = (List<Tag>)await _blogRepository.GetAllTags();
            List<string> selectedTags = new List<string>();
            PostViewModel postViewModel = new()
            {
                //BlogId = blog.BlogId,
                Tags = tags,
                AvailableTags = tags,
                SelectedTags = selectedTags,
            };
            return View(postViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreatePost(int blogId, [Bind("Title, Content, Created, BlogId, Owner, Tags, AvailableTags, SelectedTags" )] PostViewModel newPost)
        {
            var blog = _blogRepository.GetBlog(blogId);

            try
            {
                var isAuthorized = await _authorizationService.AuthorizeAsync(
                    User, blog, BlogOperations.Create);

                if (!isAuthorized.Succeeded)
                    return View("Ingentilgang");

                if (ModelState.IsValid)
                {
                    var tagsList = new List<Tag>();
                    if (newPost.SelectedTags.Count != 0)
                    {
                        string tagsToString = string.Join(",", newPost.SelectedTags);
                        tagsList = GetTagsCommaSeparated(tagsToString);
                    }

                    var post = new Post()
                    {
                        Title = newPost.Title,
                        Content = newPost.Content,
                        Created = DateTime.Now,
                        BlogId = blogId,
                        Tags = tagsList
                    };

                    _blogRepository.SavePost(post, User).Wait();
                    TempData["message"] = $"{newPost.Title} har blitt opprettet";
                    return RedirectToAction("ReadBlog", "Blog", new { id = blogId });
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return View();
            }

            TempData["message"] = "Fikk ikke opprettet ny post";
            return RedirectToAction("ReadBlog", "Blog", new { id = blogId });
        }

        public List<Tag> GetTagsCommaSeparated(string tagsStrings) //
        {
            char[] delimiterChars = { ',' };
            var tagsIdNumbers = tagsStrings.Split(delimiterChars).ToList();

            List<Tag> tagsListTemp = new List<Tag>();

            foreach (var idNumber in tagsIdNumbers)
            {
                tagsListTemp.Add(_blogRepository.GetTag(Int32.Parse(idNumber)));
            }
            return tagsListTemp;
        }


        [HttpGet]
        public async Task<ActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return NotFound("Bad parameter");
            }

            //Get the post to edit 
            var post = _blogRepository.GetPost(id);
            var emptyList = new List<string>();

            PostViewModel postViewModel = new()
            {
                PostId = post.PostId,
                Content = post.Content,
                Tags = post.Tags,
                BlogId = post.BlogId,
                Created = post.Created,
                SelectedTags = emptyList,
                Owner = post.Owner
            };

            // requires using AuthorizationHandler
            var isAuthorized = await _authorizationService.AuthorizeAsync(
                User, post, BlogOperations.Update);

            return !isAuthorized.Succeeded ? View("Ingentilgang") : View(postViewModel);
        }

        // POST: Contact/Edit/#
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditPost(int? id, [Bind("PostId, Title, Content, Created, Modified, BlogId, Tags, AvailableTags, SelectedTags, Owner")] PostViewModel postViewModel)
        {
            try
            {

                if (id == null)
                {
                    return NotFound();
                }

                Post post = _blogRepository.GetPost(postViewModel.PostId);
                Blogg blog = _blogRepository.GetBlog(post.BlogId);
                post.Blog = blog; 

                var tagsList = new List<Tag>();
                if (postViewModel.SelectedTags.Count != 0)
                {
                    var tagsToString = string.Join(",", postViewModel.SelectedTags);
                    tagsList = GetTagsCommaSeparated(tagsToString);
                }


                var isAuthorized = await _authorizationService.AuthorizeAsync(User, post, BlogOperations.Update);
                if (!isAuthorized.Succeeded)
                {
                    return View("IngenTilgang");
                }

                if (ModelState.IsValid)
                {
                    post.Content = postViewModel.Content;
                    post.Title = postViewModel.Title;
                    post.Modified = DateTime.Now;
                    post.Tags = tagsList;

                    _blogRepository.UpdatePost(post).Wait();
                    TempData["message"] = $"{post.Title} er oppdatert";
                    return RedirectToAction("ReadBlog", "Blog", new { id = post.BlogId });

                }

                return RedirectToAction("Index", "Blog");

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return View(postViewModel);
            }

        }

        [HttpGet]
        public async Task<ActionResult> DeletePost(int id)
        {
            var post = _blogRepository.GetPost(id);
            var isAuthorized = await _authorizationService.AuthorizeAsync(User, post, BlogOperations.Delete);

            if (!isAuthorized.Succeeded)
            {
                //return View("IngenTilgang");
                return RedirectToAction("NotAllowed", "Post");
            }


            var postToDelete = _blogRepository.GetPost(id);
            return View(postToDelete);
        }

        // POST:
        // Post/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeletePost(int id, IFormCollection collection)
        {

            var post = _blogRepository.GetPost(id);
            var isAuthorized = await _authorizationService.AuthorizeAsync(User, post, BlogOperations.Delete);

            if (!isAuthorized.Succeeded)
            {
                //return View("IngenTilgang");
                return RedirectToAction("NotAllowed", "Post");
            }


            try
            {
                if (ModelState.IsValid)
                {
                    //var owner = User;
                    var postToDelete = _blogRepository.GetPost(id);
                    var blogId = postToDelete.BlogId;
                    _blogRepository.DeletePost(postToDelete).Wait();
                    TempData["message"] = $"{postToDelete.Title} er slettet";

                    return RedirectToAction("ReadBlog", "Blog", new { id = blogId });

                }
                return new ChallengeResult();
            }
            catch
            {
                return ViewBag("Exception thrown");
            }
        }

        public IActionResult NotAllowed()
        {
            return View("IngenTilgang");
        }

        [AllowAnonymous]
        public ActionResult FindPostsWithTag(int tagId, int blogId)
        {
            Blogg blog = _blogRepository.GetBlog(blogId);
            List<Post> posts = _blogRepository.GetAllPostsInThisBlogWithThisTag(tagId, blogId).ToList();
            List<Tag> tagsForThisBlog = _blogRepository.GetAllTagsForBlog(blog.BlogId).ToList();

            if (ModelState.IsValid)
            {
                BloggViewModel blogViewModel = new()
                {

                    BlogId = blog.BlogId,
                    Name = blog.Name,
                    Title = (from p in posts where p.BlogId == blogId select p.Title).ToString(),
                    Created = blog.Created,
                    Modified = blog.Modified,
                    ClosedForPosts = blog.ClosedForPosts,
                    Owner = blog.Owner,
                    Posts = posts.ToList(),
                    Tags = tagsForThisBlog
                };
                return View(blogViewModel);
            }

            TempData["Feedback"] = "Feil ved søk, feil i Model: " + blog.BlogId;
            return RedirectToAction("ReadBlog", "Blog", new { id = blog.BlogId });
        }
    }
}
