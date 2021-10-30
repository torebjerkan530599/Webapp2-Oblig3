using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Models.Entities
{
    public class Comment : IAuthorizationEntity
    {
        [Key]
        public int CommentId { get; set; }

        [Required]
        [StringLength(300)]
        public string Text { get; set; }

        //
        [Column(TypeName = "datetime2")]
        public DateTime Created { get; set; }


        [Column(TypeName = "datetime2")]
        public DateTime Modified { get; set; }

        //Navigational Properties
        [Required]
        //[Display(Name = "Blogg")]
        public int PostId { get; set; }
        public virtual Post Post { get; set; }

        [JsonIgnore]
        public virtual ApplicationUser Owner { get; set; }
    }
}
