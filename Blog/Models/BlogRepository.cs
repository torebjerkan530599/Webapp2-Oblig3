using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Data;
using Blog.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blog.Models
{
    public class BlogRepository : IBlogRepository
    {
        private BlogDbContext _db;
        public BlogRepository(BlogDbContext db)
        {
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

            IEnumerable<Blogg> blogs = _db.Blogs;//.Include("Posts");
            return blogs;
        }

        public void Save(Blogg blog)
        {
            throw new NotImplementedException();
            //Merk at entitet er endret
            //blog.Modified = DateTime.Now;
            //db.Entry(product).State = EntityState.Modified;
            //db.SaveChanges();
        }
    }
}
