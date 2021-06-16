using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System;
using Core.Models;
using Core.Interfaces;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using API.Dtos;
using API.Extensions;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ITokenService _tokenService;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ITokenService tokenService)
        {
            _tokenService = tokenService;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var email = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            var user = await _userManager.FindByEmailAsync(email);
            var isInAdminRole = await _userManager.IsInRoleAsync(user, "Admin");

            return new UserDto
            {
                Email = user.Email,
                Username = user.UserName,
                Token = _tokenService.CreateToken(user),
                isAdmin = await _userManager.IsInRoleAsync(user, "Admin")
            };
        }

        [HttpPost("LoginTest")]
        public async Task<ActionResult<UserDto>> LoginTest2(User userInfo)
        {
            var user = await _userManager.FindByEmailAsync(userInfo.Email);

            if (user != null)
            {
                var result = await _signInManager.CheckPasswordSignInAsync(user, userInfo.Password, false);

                if (result.Succeeded)
                {
                    return new UserDto
                    {
                        Email = user.Email,
                        Username = user.UserName,
                        Token = _tokenService.CreateToken(user),
                        isAdmin = await _userManager.IsInRoleAsync(user, "Admin")
                    };
                }
            }

            return Unauthorized("Failed while login.");
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(User userInfo)
        {
            var user = await _userManager.FindByEmailAsync(userInfo.Email);

            if (user != null)
            {
                var result = await _signInManager.CheckPasswordSignInAsync(user, userInfo.Password, false);

                if (result.Succeeded)
                {
                    return new UserDto
                    {
                        Email = user.Email,
                        Username = user.UserName,
                        Token = _tokenService.CreateToken(user),
                        isAdmin = await _userManager.IsInRoleAsync(user, "Admin")
                    };
                }
            }

            return Unauthorized("Failed while login.");
        }

        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(User userInfo)
        {
            var userExists = await _userManager.FindByEmailAsync(userInfo.Email);
            if (userExists != null)
                return BadRequest("Email already in use."); 

            var user = new IdentityUser
            {
                UserName = userInfo.Email,
                Email = userInfo.Email
            };

            var result = await _userManager.CreateAsync(user, userInfo.Password);

            if (!result.Succeeded)
            {
                string errors = "";
                foreach (var item in result.Errors)
                    errors += item.Description + " ";
                
                return BadRequest(errors);
            }

            var signInResult = await _signInManager.CheckPasswordSignInAsync(user, userInfo.Password, false);

            if (signInResult.Succeeded)
            {
                 return new UserDto
                    {
                        Email = user.Email,
                        Username = user.UserName,
                        Token = _tokenService.CreateToken(user),
                        isAdmin = await _userManager.IsInRoleAsync(user, "Admin")
                    };
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