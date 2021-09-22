using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Models.Entities
{
    public class Comment
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
        [Display(Name = "Blogg")]
        public int PostId { get; set; }
        public virtual Post Post{ get; set; }
    }
}
