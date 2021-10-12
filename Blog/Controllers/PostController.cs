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
            return View(postViewModel);
        }


        [HttpGet]
        public ActionResult CreatePost(int blogId)
        {

            var blog = _blogRepository.GetBlog(blogId);

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
        public ActionResult CreatePost(int blogId,[Bind("Title, Content, Created, BlogId, Owner")]PostViewModel  newPost)
        {
            try
            {

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
            if(id == null)
            {
                return NotFound();
            }

            
            /*var postForTestingOwner = _blogRepository.GetPost(id);
            var isAuthorized = await _authorizationService.AuthorizeAsync(User, postForTestingOwner, BlogOperations.Update);

            if (!isAuthorized.Succeeded)
            {
                //return View("IngenTilgang");
                return new ChallengeResult();
            }*/
            var blogId = post.BlogId;

            try 
            {
                if (ModelState.IsValid)
                {
             
                    //post.Modified = DateTime.Now;
                    _blogRepository.UpdatePost(post).Wait();
                    TempData["message"] = $"{post.Title} er oppdatert";
                    return RedirectToAction("ReadBlog","Blog", new { id = blogId });
          
                }

                return RedirectToAction("Index","Blog");
                    //return View("Index");

            } catch (Exception e){
                Console.WriteLine(e.ToString());
                return View(post);
            }

        }

        [HttpGet]
        public ActionResult DeletePost(int id)
        {
            var postToDelete = _blogRepository.GetPost(id);
            return View(postToDelete);
        }

        // POST:
        // Post/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id, IFormCollection collection)
        {
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
    }
}
