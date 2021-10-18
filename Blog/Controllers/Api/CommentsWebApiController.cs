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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace Blog.Controllers.Api
{
    [Route("api/CommentsWebApi")]//[Route("api/[controller]")]
    [ApiController]
    public class CommentsWebApiController : ControllerBase //or just Controller?
    {
        private readonly BlogDbContext _context;
        private readonly IBlogRepository _repo;

        
        public CommentsWebApiController(BlogDbContext context,IBlogRepository repo) //hvordan gjøre DI med WebApi for å bruke repo?
        {
            _context = context;
            _repo = repo;
        }

        //public CommentsWebApiController(IBlogRepository repo) => _repo = repo;//hvordan får jeg alle metodene til å bruke repo?

        // GET: api/CommentsWebApi
        [HttpGet]
        [AllowAnonymous]
        public async Task<IEnumerable<Comment>> GetComments()
        {
            return await _repo.GetAllComments();
        }

        //Gets just a single comment identified by it's id
        // GET: api/CommentsWebApi/5
        /*[Produces(typeof(Comment))]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Comment>> GetComment(int id, [FromServices] 
            ILogger<CommentsWebApiController> logger) //se appsettings.json for loglevel
        {
            logger.LogDebug("GetProduct Action Invoked"); //burde skrive til output vinduet ved debugging
            //return _context.Comments.FirstOrDefault();
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            //return comment;
            return Ok(comment);
        }*/

        [Produces(typeof(IEnumerable<Comment>))] //no idea what this produces
        [HttpGet("{postIdToGet:int}")]
        [AllowAnonymous]
        public async Task<IEnumerable <Comment>> GetComments(int postIdToGet)  //preferrably it should be IHttpActionResult....
        {
            var commentsOnPost = await _repo.GetAllCommentsOnPost(postIdToGet);
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
                await _repo.UpdateComment(comment)/*.Wait()*/;
                //await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommentExists(id))
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
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<Comment>> PostComment(Comment comment)
        {
            //_context.Comments.Add(comment);
            await _repo.SaveComment(comment, User)/*.Wait()*/;
            //await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetComments), new { id = comment.CommentId }/*, comment*/); //Hvordan virker redirectAtACtion
        }

        // DELETE: api/CommentsWebApi/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteComment([FromRoute] int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CommentExists(int id)
        {
            return _context.Comments.Any(e => e.CommentId == id);
        }
    }
}
