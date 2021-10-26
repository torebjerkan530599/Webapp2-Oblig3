using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Data;
using Blog.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Web.Http;
using Blog.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Blog.Controllers.Api
{
    [Route("api/[controller]")]//alternativt [Route("api/CommentsWebApi")]
    [ApiController]
    public class CommentsWebApiController : ControllerBase //or just Controller?
    {
        private readonly IBlogRepository _repo;

        public CommentsWebApiController(IBlogRepository repo) => _repo = repo;//hvordan får jeg alle over på denne konstruksjonen?

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
        [Route("api/CommentsWebApi/FetchSingleComment/{id:int}")]
        public async Task<ActionResult<Comment>> FetchSingleComment([FromRoute]int id)
        {
            //return _context.Comments.FirstOrDefault();
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }
            return Ok(comment);
        }*/

        [Produces(typeof(IEnumerable<Comment>))]
        [HttpGet("{postId:int}")]
        [AllowAnonymous]
        public async Task<IEnumerable <Comment>> GetComments([FromRoute] int postId)  //preferrably it should be IHttpActionResult....
        {
            var commentsOnPost = await _repo.GetAllCommentsOnPost(postId);
            return commentsOnPost; //....so it could return Ok(commentsOnPost)...also simplifies unit testing.
        }


        /*[Produces(typeof(IEnumerable<Comment>))]
        [HttpGet("{postIdToGet:int}")]
        public ActionResult<IEnumerable <Comment>> GetComment(int postIdToGet, [FromServices] //this wont run asyncronously at the current moment
            ILogger<CommentsWebApiController> logger) //se appsettings.json for loglevel
        {
            logger.LogDebug("GetProduct Action Invoked"); //burde skrive til output vinduet ved debugging

            var commentsQuery = _context.Comments.Include(c => c.Post).Include(o=>o.Owner).Where(c => c.PostId == postIdToGet);
            return Ok(commentsQuery);
        }*/

        // PUT: api/CommentsWebApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutComment([FromRoute] int id, [FromBody] Comment comment)
        {
            if (id != comment.CommentId)
            {
                return BadRequest();
            }

            //_context.Entry(comment).State = EntityState.Modified;
            

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



        
        //await _repo.SaveComment(comment)/*.Wait()*/;
        // POST: api/CommentsWebApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [AllowAnonymous] //must be removed. User must be logged in
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<Comment>> PostComment([FromBody] Comment comment)
        {
            // Create new comment object containing required fields, ref createmethod mvc.
            var newComment = new Comment
            {
                Text = comment.Text,
                PostId = comment.PostId,
                Created = DateTime.Now,
            };
            
            await _repo.SaveComment(newComment); //must include User later
            
            //return StatusCode(201);
            return CreatedAtAction(nameof(GetComments) , new { id = newComment.CommentId }); 
            //return CreatedAtAction(nameof(GetSingleComment), new {id = newComment.CommentId} ,newComment); //with route specified


            //https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-5.0&tabs=visual-studio#prevent-over-posting-1
        }

        // DELETE: api/CommentsWebApi/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteComment([FromRoute] int id)
        {
            var comment = _repo.GetComment(id);
            //var comment = await _context.Comments.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            
            await _repo.DeleteComment(comment);
            //_context.Comments.Remove(comment);
            //await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
