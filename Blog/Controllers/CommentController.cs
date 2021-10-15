using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Blog.Authorization;
using Blog.Models;
using Blog.Models.Entities;
using Blog.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace Blog.Controllers
{
    public class CommentController : Controller
    {
        private readonly IBlogRepository _blogRepository;
        readonly IAuthorizationService _authorizationService;

        public CommentController(IBlogRepository blogRepository, IAuthorizationService authorizationService=null) 
        {
            _blogRepository = blogRepository;
            _authorizationService = authorizationService;
        }

        // GET: Comment/CreateComment/5
        [HttpGet]
        public ActionResult CreateComment(int PostId)
        {
            return View();
        }

        // POST: Comment/CreateComment/5
        [HttpPost]
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

                //fordi at det går an å kommentere på andre sine poster må man oppdatere PostViewModel som brukes i ReadPost

                _blogRepository.SaveComment(comment, User).Wait();

                TempData["message"] = $"kommentaren har blitt opprettet";
                TempData["username"] = comment.Owner.UserName; //just for testing purposes. Used in ReadPost action methon in PostController.
                return RedirectToAction("ReadPost", "Post", new { id = PostId });
            }
            catch 
            {
                return View();
            }
        }

        // GET:
        // Post/Comment/EditComment/5
        [HttpGet]
        public async Task<ActionResult> EditComment(int CommentId)
        {
            var commentToEdit = _blogRepository.GetComment(CommentId);

            var isAutorized = await _authorizationService.AuthorizeAsync(User, commentToEdit, BlogOperations.Update);
            if (!isAutorized.Succeeded)
            {
                return View("Ingentilgang");
            }

            return View(commentToEdit);
        }

        // POST:
        // Post/Comment/EditComment/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditComment(int? CommentId, int PostId,[Bind("CommentId, Text, Created, Modified, PostId, Post, Owner")] Comment comment)
        {
            if (CommentId == null) {
                return NotFound();
            }
            //var postId = comment.PostId;
            try
            {
                if (!ModelState.IsValid) return new ChallengeResult();

                comment.Modified = DateTime.Now;
                _blogRepository.UpdateComment(comment).Wait();
                TempData["message"] = $"{comment.Text} has been updated";

                return RedirectToAction("ReadPost", "Post", new { id = PostId });

            } catch (Exception e) {
                Console.WriteLine(e.ToString());
                return View();
            }
        }

        // GET:
        // Post/Comment/DeleteComment/5
        [HttpGet]
        public async Task<ActionResult> DeleteComment(int CommentId)
        {
            var commentToDelete = _blogRepository.GetComment(CommentId);

            var isAutorized = await _authorizationService.AuthorizeAsync(User, commentToDelete, BlogOperations.Update);
            if (!isAutorized.Succeeded)
            {
                return View("Ingentilgang");
            }

            return View(commentToDelete);
        }
        // POST:
        // Post/Comment/DeleteComment/5
        [HttpPost/*, ActionName("DeleteComment")*/]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteComment(int CommentId, IFormCollection collection)
        {

            var commentToDelete = _blogRepository.GetComment(CommentId);

            var isAutorized = await _authorizationService.AuthorizeAsync(User, commentToDelete, BlogOperations.Update);
            if (!isAutorized.Succeeded)
            {
                return View("Ingentilgang");
            }

            try
            {
                if (!ModelState.IsValid) return new ChallengeResult();
                //var owner = User;
                //var commentToDelete = _blogRepository.GetComment(CommentId);
                var postId = commentToDelete.PostId;

                _blogRepository.DeleteComment(commentToDelete).Wait();
                TempData["message"] = "Kommentaren er slettet";

                return RedirectToAction("ReadPost", "Post", new { id = postId });

            } catch {
                return ViewBag("Fikk ikke slettet kommentar");
            }
        }
    }
}
