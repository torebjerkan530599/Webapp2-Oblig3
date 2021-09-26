using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Blog.Models.Entities;
using Microsoft.AspNetCore.Identity;

namespace Blog.Models.ViewModels
{
    public class BloggViewModel
    {
        public int BlogId { get; set; }

        //Name of the blog? Nooo
        public string Name { get; set; }

        //Title of the post
        public string Title { get; set; }

        //
        public string Content { get; set; }
        //public bool? ClosedForPosts { get; set; }

        //Datoer og tid:
        [Column(TypeName = "datetime2")]
        public DateTime Created { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime Modified { get; set; }

        public virtual List<Post> Posts { get; set; }

        public virtual IdentityUser Owner { get; set;}
    }
}
