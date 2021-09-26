using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Blog.Models.Entities;
using Microsoft.AspNetCore.Identity;

namespace Blog.Models.ViewModels
{
    public class CreatePostViewModel
    {
        public int PostId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Title { get; set; }

        /*[Required]
        [StringLength(1000)]*/
        public string Content { get; set; }

        public DateTime Created { get; internal set; }

        public DateTime? Modified { get; internal set; }

        public int BlogId { get; set; }

        public virtual Blogg Blog { get; set; }

        public virtual List<Comment> Comments { get; set; }

        public virtual IdentityUser Owner { get; set; }
    }
}
