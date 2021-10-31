using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Models.Entities
{
    public class BlogApplicationUser
    {
        public string OwnerId { get; set; }
        public ApplicationUser Owner { get; set; }
        public int BlogId { get; set; }
        public Blogg Blog { get; set; }
    }
}
