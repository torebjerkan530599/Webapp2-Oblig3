using Blog.Models;
using Blog.Models.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Blog.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AccountsController : Controller
    {
        private readonly IAccountsRepository _accountsRepo;

        public AccountsController(IAccountsRepository accountsRepo)
        {
            _accountsRepo = accountsRepo;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("verifyLogin")]
        public async Task<IActionResult> VerifyLogin(ApplicationUser user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ApplicationUser res = await _accountsRepo.VerifyCredentials(user);

            if (res == null)
            {
                return Ok(new { res = "Brukernavn/Passord er feil" });
            }

            return new ObjectResult(_accountsRepo.GenerateJwtToken(res));
        }
    }
}
