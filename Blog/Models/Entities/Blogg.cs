using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Models.Entities
{
    public class Blogg : IAuthorizationEntity
    {
        [Key]
        public int BlogId { get; set; }

        [Required]
        [StringLength(50)]
        [Column("Name")]
        [Display(Name = "Blogg")]
        public string Name { get; set; }
        public bool ClosedForPosts { get; set; }

        //Datoer og tid:
        [Column(TypeName = "datetime2")]
        public DateTime Created { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime Modified { get; set; }


        //Navigational Properties
        //public int PostId { get; set; }

        //public virtual Post Post { get; set; }

        public virtual List<Post> Posts { get; set; }
        //foreign key: FK_dbo.Blog_dbo.AspNetUsers_owner_Id (id)
        //[JsonIgnore]
        //public virtual ApplicationUser Owner { get; set;}

        public virtual ApplicationUser Owner { get; set; }

        public virtual ICollection<BlogApplicationUser> BlogApplicationUsers { get; set; } =
            new List<BlogApplicationUser>();
    }
}
