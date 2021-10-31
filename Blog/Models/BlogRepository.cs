using Blog.Data;
using Blog.Models.Entities;
using Blog.Models.ViewModels;
using Blog.Util;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Blog.Models
{
    public class BlogRepository : IBlogRepository
    {
        private readonly BlogDbContext _db;
        private UserManager<ApplicationUser> _manager;
        public BlogRepository(UserManager<ApplicationUser> userManager, BlogDbContext db)
        {
            _manager = userManager;
            _db = db;
            //_ = new SeedData(_db);
        }

        public async Task<IEnumerable<Blogg>> GetAllBlogs()
        {
            IEnumerable<Blogg> blogs = await _db.Blogs.Include(o => o.Owner).ToListAsync(); ;
            return blogs;
        }

        public Blogg GetBlog(int blogId)
        {
            IEnumerable<Blogg> blogs = _db.Blogs.Include(o => o.Owner);
            var singleBlogQuery = from blog in blogs
                                  where blog.BlogId == blogId
                                  select blog;
            return singleBlogQuery.FirstOrDefault();
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
                p = _db.Blogs.Include(blogg => blogg.Posts)
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
                p = _db.Posts.Include(o => o.Comments).Include(o => o.Owner)
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


        public IEnumerable<Post> GetAllPosts(int blogId)
        {

            IEnumerable<Post> posts = _db.Posts;
            var postQuery = from post in posts
                            where post.BlogId == blogId
                            orderby post.Created descending
                            select post;
            return postQuery;

        }

        public Post GetPost(int? id)
        {
            return ((from p in _db.Posts
                     where p.PostId == id
                     select p)).Include(o => o.Owner).Include(p=>p.Tags).FirstOrDefault();
        }

        public async Task<IEnumerable<Comment>> GetAllComments() //gets all comments, not just the comments on a specific post
        {
            IEnumerable<Comment> comments = await _db.Comments.ToListAsync(); ;
            return comments;
        }

        public async Task<IEnumerable<Comment>> GetAllCommentsOnPost(int postIdToGet)
        {
            var post = await _db.Posts.Include(c => c.Comments).Include(o => o.Owner).FirstAsync(x => x.PostId == postIdToGet);
            return post.Comments;
        }
        
        public Comment GetComment(int commentIdToGet)
        {

            IEnumerable<Comment> comments = _db.Comments.Include(o => o.Owner);
            var singleCommentQuery = from comment in comments
                                     where comment.CommentId == commentIdToGet
                                     select comment;
            return singleCommentQuery.FirstOrDefault();
        }

        /*public async Task GetComment(int commentIdToGet)
        {
            return await _db.Comments.FindAsync(commentIdToGet);
        }*/

        //internal use for MVC model
        private IEnumerable<Comment> GetAllComments(int? postIdToGet)
        {
            IEnumerable<Comment> comments = _db.Comments.Include(o => o.Owner);
            var commentsQuery = from comment in comments
                                where comment.PostId == postIdToGet
                                orderby comment.Created descending
                                select comment;
            return commentsQuery;
        }

        public async Task<IEnumerable<Tag>> GetAllTags()
        {
            List<Tag> tags = await _db.Tags.Include(t => t.Posts).ToListAsync();
            return tags;
        }
        //Hent alle tags for en blogg
        public IEnumerable<Tag> GetAllTagsForBlog(int blogId)
        {

            var tagsToShow = new List<Tag>();
            foreach (var tag in _db.Tags.Include(a => a.Posts)) //Henter alle tags
            {
                foreach (var tagPost in tag.Posts)  //Traverserer alle poster inne i hver tag
                {
                    if (tagPost.BlogId == blogId) //Legger i lista de som tilhører denne blogggen
                    {
                        if (!tagsToShow.Contains(tag))
                        {
                            tagsToShow.Add(tag);
                        }
                    }
                }
            }
            return tagsToShow;
        }

        public IEnumerable<Post> GetAllPostsInThisBlogWithThisTag(int tagId, int blogId)
        {
            List<Post> posts = (from p in _db.Posts.Include(p => p.Tags)
                                where p.BlogId == blogId
                                select p).ToList();

            List<Post> postsToShow = new();

            foreach (var post in posts)
            {
                foreach (var postTags in post.Tags)
                {
                    if (postTags.TagId == tagId)
                    {
                        postsToShow.Add(post);
                    }
                }
            }
            return postsToShow;
        }

        public Tag GetTag(int tagIdToGet)
        {
            var tagQuery = (from tag in _db.Tags
                            where tag.TagId == tagIdToGet
                            select tag).Include(o => o.Posts);
            return tagQuery.FirstOrDefault();
        }

        public async Task SaveBlog(Blogg blog, ClaimsPrincipal user)
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

        public async Task<bool> SaveComment(Comment comment)
        {
            _db.Comments.Add(comment);
            return (await _db.SaveChangesAsync() > 0);

        }

        public async Task UpdatePost(Post post)
        {

            _db.Entry(post).State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }

        public async Task UpdateComment(Comment comment)
        {

            _db.Entry(comment).State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }

        public async Task DeletePost(Post post)
        {
            _db.Posts.Remove(post);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteComment(Comment comment)
        {
            _db.Comments.Remove(comment);
            await _db.SaveChangesAsync();

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

        public bool CommentExists(int id)
        {
            return _db.Comments.Any(e => e.CommentId == id);
        }
    }
}
