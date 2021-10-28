using Blog.Models.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Models.ViewModels
{
    public class PostViewModel
    {
        public int PostId { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime Created { get; internal set; }

        [Column(TypeName = "datetime2")]
        public DateTime? Modified { get; internal set; }

        public int BlogId { get; set; }

        public virtual Blogg Blog { get; set; }

        public virtual List<Comment> Comments { get; set; }

        public virtual IdentityUser Owner { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
        public IList<string> SelectedTags { get; set; }
        public IList<Tag> AvailableTags { get; set; }
    }
}
