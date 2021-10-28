using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using Blog.Models.Entities;

namespace Blog.Models.ViewModels
{
    public class CreateBloggViewModel
    {
        public int BlogId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        public bool ClosedForPosts { get; set; }
        //public string Description { get; set; } not a requirement

        public DateTime Created { get; internal set; }

        public ApplicationUser Owner { get; set; }



    }
}
