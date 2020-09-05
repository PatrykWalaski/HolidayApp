using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System;
using Core.Models;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;

        }

        [HttpPost("Login")]
        public async Task<ActionResult> Login(User userInfo)
        {
            var user = await _userManager.FindByEmailAsync(userInfo.Email);

            if (user != null)
            {
                var signInResult = await _signInManager.PasswordSignInAsync(user, userInfo.Password, false, false);

                if(signInResult.Succeeded)
                {
                    return Ok(user);
                }
            }

            return Unauthorized("Failed while login.");
        }

        [HttpPost("Register")]
        public async Task<ActionResult<User>> Register(User userInfo)
        {

            var user = new IdentityUser
            {
                UserName = userInfo.Email,
                Email = userInfo.Email
            };

            var result = await _userManager.CreateAsync(user, userInfo.Password);

            if (result.Succeeded)
            {
                var signInResult = await _signInManager.PasswordSignInAsync(user, userInfo.Password, false, false);

                if(signInResult.Succeeded)
                {
                    return Ok(user);
                }
            }

            return BadRequest("Failed on register.");

        }

        [Route("Logout")]
        public async Task<ActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }
    }
}