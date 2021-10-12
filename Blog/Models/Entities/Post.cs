using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Blog.Models.Entities
{
    public class Post : IAuthorizationEntity
    {
        [Key]
        public int PostId { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Innlegg")]
        public string Title { get; set; }

        [Required]
        [StringLength(1000)]
        public string Content { get; set; }

        //
        [Column(TypeName = "datetime2")]
        public DateTime Created { get; set; }

        public int NumberOfComments { get; set; }
        
        [Column(TypeName = "datetime2")]
        public DateTime Modified { get; set; }
        
        //Navigational Properties
        public int BlogId { get; set; }
        public virtual Blogg Blog{ get; set; }
        public virtual List<Comment> Comments { get; set; }

        public virtual IdentityUser Owner { get; set;}
    }
}
