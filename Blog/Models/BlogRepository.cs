using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Blog.Data;
using Blog.Models.Entities;
using Blog.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Blog.Models
{
    public class BlogRepository : IBlogRepository
    {
        private readonly BlogDbContext _db;
        private UserManager<IdentityUser> _manager;
        public BlogRepository(UserManager<IdentityUser> userManager, BlogDbContext db)
        {
            _manager = userManager;
            _db = db;
        }

        public async Task<IEnumerable<Blogg>> GetAllBlogs()
        {

            IEnumerable<Blogg> blogs = await _db.Blogs.Include(o => o.Owner).ToListAsync();;
            return blogs;
        }

        public IEnumerable<Post> GetAllPosts(int blogId) //presents all posts to user
        {

            IEnumerable<Post> posts = _db.Posts;
            var postQuery = from post in posts
                where post.BlogId == blogId
                orderby post.Created descending
                select post;

            return postQuery;

        }

        public IEnumerable<Comment> GetAllComments(int? postIdToGet)
        {
            IEnumerable<Comment> comments = _db.Comments.Include(o => o.Owner);
            var commentsQuery = from comment in comments
                where comment.PostId == postIdToGet
                orderby comment.Created descending 
                select comment;
            return commentsQuery;
        }


        public Blogg GetBlog(int blogId)
        {
            IEnumerable<Blogg> blogs = _db.Blogs.Include(o=>o.Owner);
            var singleBlogQuery = from blog in blogs
                where blog.BlogId == blogId
                select blog;
            return singleBlogQuery.FirstOrDefault();
        }

        public Post GetPost(int? id)
        {
            return ((from p in _db.Posts
                where p.PostId == id
                select p)).Include(o=>o.Owner).FirstOrDefault();
        }

        //GET COMMENT
        public Comment GetComment(int commentIdToGet)
        {

            IEnumerable<Comment> comments = _db.Comments.Include(o=>o.Owner);
            var singleCommentQuery = from comment in comments
                where comment.CommentId == commentIdToGet
                select comment;
            return singleCommentQuery.FirstOrDefault();
        }

        public async Task SaveBlog(Blogg blog,  ClaimsPrincipal user)
        {
            var currentUser = await _manager.FindByNameAsync(user.Identity?.Name);
            blog.Owner = currentUser;
            _db.Blogs.Add(blog);
            await _db.SaveChangesAsync();
        }

        public async Task SavePost(Post post, ClaimsPrincipal user)
        {
            var currentUser = await _manager.FindByNameAsync(user.Identity?.Name);
            post.Owner = currentUser;
            _db.Posts.Add(post);
            await _db.SaveChangesAsync();
        }

        public async Task SaveComment(Comment comment, ClaimsPrincipal user)
        {
            var currentUser = await _manager.FindByNameAsync(user.Identity?.Name);
            comment.Owner = currentUser;
            _db.Comments.Add(comment);
            await _db.SaveChangesAsync();
        }

        //UPDATE POST
        public async Task UpdatePost(Post post)
        {

            _db.Entry(post).State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }

        //UPDATE COMMENT
        public async Task UpdateComment(Comment comment)
        {

            _db.Entry(comment).State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }


        //DELETE POST
        public async Task DeletePost(Post post)
        {
            _db.Posts.Remove(post);
            await _db.SaveChangesAsync();
        }

    
        //DELETE COMMENT
        public async Task DeleteComment(Comment comment)
        {
            _db.Comments.Remove(comment);
            await _db.SaveChangesAsync();

        }

        public CreateBloggViewModel GetCreateBlogViewModel(int? id)
        {
            CreateBloggViewModel p;
            if (id == null)
            {
                p = new CreateBloggViewModel();
            }

            else
            {
                p = _db.Blogs.Include(blogg=>blogg.Posts)
                        .Where(b => b.BlogId == id)
                        .Select(k => new CreateBloggViewModel()
                            {
                                BlogId = k.BlogId,
                                Name = k.Name,
                                Created = DateTime.Now
                            }
                        ).FirstOrDefault();
            }
            return p;
        }

        public PostViewModel GetPostViewModel(int? id)
        {
            PostViewModel p;
            if (id == null)
            {
                p = new PostViewModel();
            }
            else
            {
                p = _db.Posts.Include(o=>o.Comments).Include(o=>o.Owner)
                    .Where(o => o.PostId == id)
                    .Select(o => new PostViewModel 
                    {
                        PostId = o.PostId,
                        Title = o.Title,
                        Content = o.Content,
                        Created = o.Created,
                        Modified = o.Modified,
                        Blog = o.Blog,
                        BlogId = o.BlogId,
                        Comments = GetAllComments(id).ToList(),
                        Owner = o.Owner
                    }).FirstOrDefault();
            }
            return p;
        }

  

        //marks the blog as either closed or open in db
        public void SetBlogStatus(Blogg blog, bool status)
        {
            blog.ClosedForPosts = status;
            var entry = _db.Entry(blog);
            entry.Property(e => e.ClosedForPosts).IsModified = true;
            _db.SaveChanges();
        }

        public bool? IsActive(Blogg blog)
        {
            return blog.ClosedForPosts;

        }

     
    }
}
