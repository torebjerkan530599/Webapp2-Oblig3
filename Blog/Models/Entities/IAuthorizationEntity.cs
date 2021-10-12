using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Blog.Models.Entities
{
    public interface IAuthorizationEntity
    {
        //string OwnerId { get; set; }

        IdentityUser Owner { get; set; }
    }
}
