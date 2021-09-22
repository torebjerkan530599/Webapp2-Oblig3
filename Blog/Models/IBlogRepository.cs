using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Models
{
    public interface IBlogRepository
    {
        IEnumerable<Entities.Blogg> GetAllBlogs();

        void Save(Entities.Blogg blog);
    }
}
