using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Blog.Authorization;
using Blog.Models;
using Blog.Models.Entities;
using Blog.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace Blog.Controllers
{
    public class PostController : Controller
    {
        private readonly IBlogRepository _blogRepository;
        readonly IAuthorizationService _authorizationService;

        public PostController(IBlogRepository blogRepository, IAuthorizationService authorizationService=null) 
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
            
            if (!closed.GetValueOrDefault(false)) //hvis ikke stengt, gå til opprett post
            {
                return View();
            }

            //hvis bloggen er stengt, gi beskjed.
            TempData["message"] = "Bloggen er stengt for innlegg";
            return RedirectToAction("ReadBlog","Blog", new {id= blogId});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreatePost(int blogId,[Bind("Title, Content, Created, BlogId, Owner")]PostViewModel  newPost)
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
                    var post = new Post()
                    {
                        Title = newPost.Title,
                        Content = newPost.Content,
                        Created = DateTime.Now,
                        BlogId = blogId,
                    };

                    _blogRepository.SavePost(post, User).Wait();
                    TempData["message"] = $"{newPost.Title} har blitt opprettet";
                    return RedirectToAction("ReadBlog","Blog", new {id= blogId});
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e); 
                return View();
            }

            TempData["message"] = "Fikk ikke opprettet ny post";
            return RedirectToAction("ReadBlog","Blog", new {id= blogId});
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


            // requires using AuthorizationHandler
            var isAuthorized = await _authorizationService.AuthorizeAsync(
                User, post, BlogOperations.Update);

            return !isAuthorized.Succeeded ? View("Ingentilgang") : View(post);
        }

        // POST: Contact/Edit/#
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditPost(int? id, [Bind("PostId, Title, Content, Modified, BlogId, Owner")]Post post)
        {
            try 
            {

                if(id == null)
                {
                    return NotFound();
                }

                var content = post.Content;
                var title = post.Title;

                var editedPost = _blogRepository.GetPost(id);
                editedPost.Content = content;
                editedPost.Title = title;

                var isAuthorized = await _authorizationService.AuthorizeAsync(User, editedPost, BlogOperations.Update);
                if (!isAuthorized.Succeeded)
                {
                    return View("IngenTilgang");
                }
                
                var blogId = post.BlogId;

                if (ModelState.IsValid)
                {
             
                    //post.Modified = DateTime.Now;
                    _blogRepository.UpdatePost(editedPost).Wait();
                    TempData["message"] = $"{post.Title} er oppdatert";
                    return RedirectToAction("ReadBlog","Blog", new { id = blogId });
          
                }

                return RedirectToAction("Index","Blog");

            } catch (Exception e){
                Console.WriteLine(e.ToString());
                return View(post);
            }

        }

        [HttpGet]
        public async Task<ActionResult> DeletePost(int id)
        {
            var post = _blogRepository.GetPost(id);
            var isAuthorized =  await _authorizationService.AuthorizeAsync(User, post, BlogOperations.Delete);
            
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
            var isAuthorized =  await _authorizationService.AuthorizeAsync(User, post, BlogOperations.Delete);
            
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
    }
}
