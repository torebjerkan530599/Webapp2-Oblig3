﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Blog.Models.Entities;

namespace Blog.Models.ViewModels
{
    public class CreateBloggViewModel
    {
        public int BlogId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        //public string Description { get; set; } not a requirement

        public DateTime Created { get; internal set; }



    }
}
