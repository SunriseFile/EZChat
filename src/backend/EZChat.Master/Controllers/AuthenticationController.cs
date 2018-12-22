using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

using EZChat.Master.Controllers.Models;
using EZChat.Master.Identity.Models;
using EZChat.Master.Identity.Services;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EZChat.Master.Controllers
{
    [Route("auth")]
    public class AuthenticationController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IJsonWebTokenGenerator _tokenGenerator;

        public AuthenticationController(UserManager<AppUser> userManager,
                              SignInManager<AppUser> signInManager,
                              IJsonWebTokenGenerator tokenGenerator)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenGenerator = tokenGenerator;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ErrorResponse(ModelState));
            }

            var user = await _userManager.FindByNameAsync(model.UserName);

            if (user == null)
            {
                return BadRequest(new ErrorResponse("Invalid username or password", ErrorResponseCode.UsernamePasswordInvalid));
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

            if (!result.Succeeded)
            {
                return BadRequest(new ErrorResponse("Invalid username or password", ErrorResponseCode.UsernamePasswordInvalid));
            }

            var token = _tokenGenerator.Generate(user);

            return Ok(token);
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ErrorResponse(ModelState));
            }

            var user = new AppUser
            {
                UserName = model.UserName,
                DisplayName = model.DisplayName
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return BadRequest(new ErrorResponse(result));
            }

            var token = _tokenGenerator.Generate(user);

            return Ok(token);
        }
    }

    public class LoginModel
    {
        [Required(ErrorMessage = "username is required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "password is required")]
        public string Password { get; set; }
    }

    public class RegisterModel
    {
        [Required(ErrorMessage = "username is required")]
        [StringLength(64, ErrorMessage = "username cannot be longer than 64 characters")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "password is required")]
        [MinLength(6, ErrorMessage = "password cannot be less than 6 characters")]
        public string Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = "passwords do not match")]
        public string PasswordConfirm { get; set; }

        [Required(ErrorMessage = "display name is required")]
        [StringLength(64, ErrorMessage = "display name cannot be longer than 64 characters")]
        public string DisplayName { get; set; }
    }
}
