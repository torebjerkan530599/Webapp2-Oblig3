using Blog.Models;
using Blog.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.Controllers.Api
{
    [Route("api/[controller]")]//alternativt [Route("api/CommentsWebApi")]
    [ApiController]
    public class CommentsWebApiController : ControllerBase //or just Controller?
    {
        private readonly IBlogRepository _repo;
        private UserManager<ApplicationUser> _userManager;

        //public CommentsWebApiController(IBlogRepository repo) => _repo = repo;//hvordan får jeg alle over på denne konstruksjonen?
        
        public CommentsWebApiController (IBlogRepository repo, UserManager<ApplicationUser> userManager)
        { 
            
                _userManager = userManager;
                _repo = repo;
        }

        // GET: api/CommentsWebApi. Returns all comments
        [HttpGet]
        [AllowAnonymous]
        public async Task<IEnumerable<Comment>> GetComments()
        {
            return await _repo.GetAllComments();
        }

        // GET: api/CommentsWebApi/5. Returns a response from the server containing the comment object.
        /*[Produces(typeof(Comment))]
        [HttpGet("{id:int}")]
        [Route("GetSingleComment/{id:int}")]
        public async Task<ActionResult<Comment>> GetSingleComment([FromRoute]int id)
        {
            //return _context.Comments.FirstOrDefault();
            var comment = _repo.GetComment(id);
            if (comment == null)
            {
                return NotFound();
            }
            return Ok(comment);
        }*/

        [Produces(typeof(IEnumerable<Comment>))]
        [HttpGet("{postId:int}")]
        public async Task<IEnumerable<Comment>> GetComments([FromRoute] int postId)  //preferrably it should be IHttpActionResult....
        {
            var commentsOnPost = await _repo.GetAllCommentsOnPost(postId);
            return commentsOnPost; //....so it could return Ok(commentsOnPost)...also simplifies unit testing.
        }

        // PUT: api/CommentsWebApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutComment([FromRoute] int id, [FromBody] Comment comment)
        {
            if (id != comment.CommentId)
            {
                return BadRequest();
            }

            try
            {
                await _repo.UpdateComment(comment);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_repo.CommentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent(); //StatusCode 204
        }




        // POST: api/CommentsWebApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        //[AllowAnonymous] //Only for testing purposes! Delete before release.
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<Comment>> PostComment([FromBody] Comment comment)
        {
            var owner = await _userManager.GetUserAsync(HttpContext.User);
            // Create new comment object containing required fields, ref createmethod mvc.
            var newComment = new Comment
            {
                Text = comment.Text,
                PostId = comment.PostId,
                Created = DateTime.Now,
                Owner = owner,
                //Post = comment.Post
            };

            if (await _repo.SaveComment(newComment))
            {
                return Ok(newComment);

            }
            else
            {
                return StatusCode(500);
            }

            //return CreatedAtAction(nameof(GetComments) , new { id = newComment.CommentId }); 
            //return CreatedAtAction(nameof(GetSingleComment), new {id = newComment.CommentId} ,newComment); //with route specified
        }

        // DELETE: api/CommentsWebApi/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteComment([FromRoute] int id)
        {
            var comment = _repo.GetComment(id);

            if (comment == null)
            {
                return NotFound();
            }

            await _repo.DeleteComment(comment);

            return NoContent();
        }
    }
}
