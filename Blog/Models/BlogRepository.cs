using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Models
{
    public class BlogRepository : IBlogRepository
    {
        public IEnumerable<Entities.Blog> GetAllBlogs()
        {
            List<Models.Entities.Blog> blogs = new()
            {
                new Models.Entities.Blog {BlogId = 1, Name = "First in line", ClosedForPosts = false},
                new Models.Entities.Blog {BlogId = 2, Name = "Everything was great", ClosedForPosts = false},
            };
            return blogs;
        }

        public void Save(Entities.Blog blog)
        {
            throw new NotImplementedException();
            //Merk at entitet er endret
            //blog.Modified = DateTime.Now;
            //db.Entry(product).State = EntityState.Modified;
            //db.SaveChanges();
        }
    }
}
