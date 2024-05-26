using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Account;
using api.Interface;
using api.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Microsoft.AspNetCore.Components.Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService  _tokenService;
        private readonly SignInManager<AppUser> _signInManager;
        public AccountController(UserManager<AppUser> userManager, ITokenService tokenService, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
        {


            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var user = new AppUser
                {
                    UserName = registerDTO.Username,
                    Email = registerDTO.Email
                };

                var createduser = await _userManager.CreateAsync(user, registerDTO.Password);

                if (createduser.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(user, "User");
                    if (roleResult.Succeeded)
                    {
                        return Ok(new NewUserDTO
                        {
                            Username = user.UserName,
                            Email = user.Email,
                            Tokens = _tokenService.CreateToken(user)
                        });
                    }
                }

                return BadRequest(createduser.Errors);
            }
            catch (System.Exception e)
            {
                return StatusCode(500, e.Message);
            }

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == loginDTO.Username);
                            
                if(user == null)
                {
                    return Unauthorized("Invalid Username");
                }

                var result = await _signInManager.CheckPasswordSignInAsync(user, loginDTO.Password, false);

                if(!result.Succeeded)
                {
                    return Unauthorized("Username not found and password is incorrect");
                }

                return Ok(new NewUserDTO
                {
                    Username = user.UserName,
                    Email = user.Email,
                    Tokens = _tokenService.CreateToken(user)
                });

            }
            catch (System.Exception e)
            {
                
                return StatusCode(500, e.Message);
            }
        }

    }
}