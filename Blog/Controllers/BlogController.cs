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
using Microsoft.AspNetCore.Identity;

namespace Blog.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogRepository _blogRepository;
        readonly IAuthorizationService _authorizationService;

        public BlogController(IBlogRepository blogRepository, IAuthorizationService authorizationService=null) 
        {
            _blogRepository = blogRepository;
            _authorizationService = authorizationService;
        }

        [AllowAnonymous]
        // GET: BlogController
        public ActionResult Index()
        {
         
            var blogs = _blogRepository.GetAllBlogs();//.ToList(); ;
            return View(blogs);
        }

        [AllowAnonymous]
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
                Posts = posts.ToList(),
                Owner = blog.Owner
            };
            
            return View(bv);
        }

        [AllowAnonymous]
        public ActionResult ReadPost(int id)
        {
            var postViewModel = _blogRepository.GetPostViewModel(id);
            return View(postViewModel);
        }


        // GET: Blogg/Create
        //[Authorize]
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Blog/Create
        [HttpPost]
        //[Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Name, ClosedForPosts, Created, Owner")]CreateBloggViewModel newBlog)
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
                    _blogRepository.SaveBlog(blog,User).Wait();
                    TempData["message"] = $"{blog.Name} har blitt opprettet";
                    return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        //[Authorize]
        [HttpGet]
        public ActionResult CreatePost(int blogId)
        {
            var closed = _blogRepository.GetBlog(blogId).ClosedForPosts;

           
            
            if (!closed.GetValueOrDefault(false)) //hvis ikke stengt, gå til opprett post
            {
                return View();
            }

            //hvis bloggen er stengt, gi beskjed.
            TempData["message"] = "Bloggen er stengt for innlegg";
            return RedirectToAction("ReadBlog", new {id= blogId});
        }

        // POST: Blog/Create
        [HttpPost]
        //[Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePost(int blogId,[Bind("Title, Content, Created, BlogId, Owner")]PostViewModel  newPost)
        {
            try
            {
                //var closed = _blogRepository.GetBlog(blogId).ClosedForPosts;
                //if (!closed.GetValueOrDefault(false)) //hvis ikke stengt, gå til opprett post {
            
                    if (ModelState.IsValid)
                    {
                        var post = new Post()
                        {
                            Title = newPost.Title,
                            Content = newPost.Content,
                            Created = DateTime.Now,
                            BlogId = blogId,
                            Owner = newPost.Owner
                        };

                        _blogRepository.SavePost(post, User).Wait();
                        TempData["message"] = $"{newPost.Title} har blitt opprettet";
                        return RedirectToAction("ReadBlog", new {id= blogId});
                    }
                //}
            }
            catch (Exception e)
            {
                Console.WriteLine(e); 
                return View();
            }

            TempData["message"] = "Fikk ikke opprettet ny post";
            return RedirectToAction("ReadBlog", new {id= blogId});
        }

        //[Authorize]
        [HttpGet]
        public ActionResult CreateComment(int PostId)
        {
            //var newBlog = new CreateBloggViewModel();//blogRepository.GetCreateBlogViewModel();
            //return View(newBlog);
            return View();
        }

        // POST: Blog/Create
        [HttpPost]
        //[Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult CreateComment(int PostId, [Bind("CommentId, Text, Created, PostId, Owner")]CommentViewModel newCommentViewModel)
        {
            try
            {
                if (!ModelState.IsValid) return View();

                var comment = new Comment()
                {
                    Text = newCommentViewModel.Text,
                    Created = DateTime.Now,
                    PostId = PostId,
                };
                
                
                _blogRepository.SaveComment(comment, User).Wait();
                TempData["message"] = "kommentaren har blitt opprettet";
                return RedirectToAction("ReadPost", new { id = PostId });
            }
            catch 
            {
                return View();
            }
        }

        // GET: Contact/Edit/#
        [HttpGet]
        public async Task<ActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return NotFound("Bad parameter");
            }
         
            //Get the post to edit 
            var post = _blogRepository.GetPost(id);
            //var ownerOfPost = post.Owner.Id;
           

                
            // requires using ContactManager.Authorization;
            var isAuthorized = await _authorizationService.AuthorizeAsync(
                User, post, BlogOperations.Update);

            if (!isAuthorized.Succeeded)
            {
                //return new ChallengeResult();
                return Forbid();
            }
            return View(post);
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

            // requires using ContactManager.Authorization;
            var isAuthorized = await _authorizationService.AuthorizeAsync(
                User, post, BlogOperations.Update);

            if (!isAuthorized.Succeeded)
            {
                return View("Ingentilgang");
            }
            var blogId = post.BlogId;

            try 
            {
                if (ModelState.IsValid)
                {
                    //var owner = post.Owner.UserName;

                    //if (owner == User.Identity.Name.ToString())
                    //{
                    post.Modified = DateTime.Now;
                    _blogRepository.UpdatePost(post).Wait();
                    TempData["message"] = $"{post.Title} er oppdatert";
                    return RedirectToAction("ReadBlog", new { id = blogId });
                    //}
                    //else
                    // {
                    //return View("Kan ikke endre andre sine post");
                    //}
                } 
                else return new ChallengeResult();
            } catch {
                return View(post);
            }

        }


                // GET:
        // Post/Delete/5
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
                        TempData["message"] = $"{postToDelete.Title} has been updated";

                        return RedirectToAction("ReadBlog", new { id = blogId });
                        
                }
                return new ChallengeResult();
            }
            catch
            {
                return ViewBag("Exception thrown");
            }
        }

        // GET:
        // Post/Comment/Delete/5
        [HttpGet]
        public ActionResult DeleteComment(int CommentId)
        {
            var commentToDelete = _blogRepository.GetComment(CommentId);
            return View(commentToDelete);
        }
        // POST:
        // Post/Comment/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteComment(int CommentId, IFormCollection collection)
        {
            try
            {
                if (!ModelState.IsValid) return new ChallengeResult();
                //var owner = User;
                var commentToDelete = _blogRepository.GetComment(CommentId);
                var postId = commentToDelete.PostId;

                _blogRepository.DeleteComment(commentToDelete).Wait();
                TempData["message"] = "Kommentaren er slettet";

                return RedirectToAction("ReadPost", new { id = postId });

            } catch {
                return ViewBag("Fikk ikke slettet kommentar");
            }
        }

        // GET:
        // Post/Coomment/Edit/5
        //[Authorize]
        [HttpGet]
        public ActionResult EditComment(int CommentId)
        {
            var commentToEdit = _blogRepository.GetComment(CommentId);

            //var isAutorized = await _authorizationService.AuthorizeAsync(User, postToEdit, BlogOperations.Update);
            //if (!isAutorized.Succeeded)
            //{
            //return View("Ingen tilgang");
            //}

            return View(commentToEdit);
        }

        // POST:
        // Post/Coomment/Edit/5
        //[Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditComment(int? CommentId, int PostId,[Bind("CommentId, Text, Created, Modified, PostId, Post, Owner")] Comment comment)
        {
            if (CommentId == null) {
                return NotFound();
            }
            var postId = comment.PostId;
            try
            {
                if (!ModelState.IsValid) return new ChallengeResult();

                comment.Modified = DateTime.Now;
                _blogRepository.UpdateComment(comment).Wait();
                TempData["message"] = $"{comment.Text} has been updated";

                return RedirectToAction("ReadPost", new { id = PostId });

            } catch {
                return View("Fikk ikke endret commentar");
            }
        }
    }
}
