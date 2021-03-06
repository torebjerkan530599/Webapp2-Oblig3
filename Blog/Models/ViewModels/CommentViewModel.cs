using Blog.Models.Entities;
using Microsoft.AspNetCore.Identity;
using System;

namespace Blog.Models.ViewModels
{
    public class CommentViewModel
    {
        public int CommentId { get; set; }

        public string Text { get; set; }

        public DateTime Created { get; internal set; }

        public DateTime? Modified { get; internal set; }

        public int PostId { get; set; }

        public virtual Post Post { get; set; }

        public virtual ApplicationUser Owner { get; set; }
    }
}
