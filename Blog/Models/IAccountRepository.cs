using System.Collections.Generic;
using System.Threading.Tasks;
using Blog.Models.Entities;
using Microsoft.AspNetCore.Identity;

namespace Blog.Models
{
    public interface IAccountsRepository
    {

        Task<ApplicationUser> VerifyCredentials(ApplicationUser user);
        string GenerateJwtToken(ApplicationUser user);
        Task<bool> ChangePasswd(ApplicationUser u, string oldP, string newP);
        Task<ApplicationUser> ChangeRole(ApplicationUser u, string newR);
        Task<bool> DeleteUser(ApplicationUser u);
        Task<ApplicationUser> AddUser(ApplicationUser u);
        Task<List<ApplicationUser>> GetAllUsers();
    }
}