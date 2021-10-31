using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Models.Entities;

namespace Blog.Models.ViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<Post> Posts { get; set; }
        public IEnumerable<Blogg> Blogs { get; set; }
        public IEnumerable<Tag> Tags { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
    }
}
