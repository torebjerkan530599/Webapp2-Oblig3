using Microsoft.AspNetCore.Identity;

namespace Blog.Models.Entities
{
    public interface IAuthorizationEntity
    {
        //string OwnerId { get; set; }

        //IdentityUser Owner { get; set; }

        IdentityUser Owner { get; set; }
    }
}
