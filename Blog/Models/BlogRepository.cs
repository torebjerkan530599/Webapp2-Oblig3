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

        public IEnumerable<Blogg> GetAllBlogs()
        {
            //************Just for testing******************
            //List<Blogg> blogs = new()
            //{
            //    new Blogg {BlogId = 1, Name = "First in line", ClosedForPosts = false},
            //    new Blogg {BlogId = 2, Name = "Everything was great", ClosedForPosts = false},
            //};

            IEnumerable<Blogg> blogs = _db.Blogs;//.Include(e=> e.Posts);
            return blogs;
        }


        public Blogg GetBlog(int blogIdToGet)
        {
            IEnumerable<Blogg> blogs = _db.Blogs;
            var singleBlogQuery = from blog in blogs
                where blog.BlogId == blogIdToGet
                select blog;
            return singleBlogQuery.FirstOrDefault();
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

        public async Task SaveBlog(Blogg blog,  ClaimsPrincipal user)
        {
            var currentUser = await _manager.FindByNameAsync(user.Identity?.Name);
            blog.Owner = currentUser;
            _db.Blogs.Add(blog);
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
                p = (_db.Blogs.Include(p=>p.Posts)
                        .Where(b => b.BlogId == id)
                        .Select(k => new CreateBloggViewModel()
                            {
                                BlogId = k.BlogId,
                                Name = k.Name,
                                Created = DateTime.Now
                            }
                        ).FirstOrDefault());
            }
            return p;
        }

    }
}
