using System.Threading.Tasks;

using EZChat.Master.Identity.Models;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EZChat.Master.Controllers
{
    [Route("user")]
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public UserController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet("current")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var user = await _userManager.FindByNameAsync(Request.HttpContext.User.Identity.Name);
            var publicUser = new PublicAppUser(user);

            return Ok(publicUser);
        }
    }
}
