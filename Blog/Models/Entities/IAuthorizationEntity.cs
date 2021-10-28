using Microsoft.AspNetCore.Identity;

namespace Blog.Models.Entities
{
    public interface IAuthorizationEntity
    {
        //string OwnerId { get; set; }

        //ApplicationUser Owner { get; set; }

        ApplicationUser Owner { get; set; }
    }
}
