using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Models
{
    public interface IBlogRepository
    {
        IEnumerable<Entities.Blog> GetAllBlogs();

        void Save(Entities.Blog blog);
    }
}
