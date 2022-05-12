using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using StoreAPI.DTO;
using StoreAPI.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace StoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly IConfiguration configuration;
        private readonly SignInManager<User> signInManager;

        public AccountController(UserManager<User> userManager,
            IConfiguration configuration , SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.configuration = configuration;
            this.signInManager = signInManager;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDTO register)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            User userModel = new User()
            {
                UserName = register.UserName,
                Email = register.Email,
            };
            IdentityResult result = await userManager.CreateAsync(userModel, register.Password);
            if (result.Succeeded)
            {
                return Ok("Add Success");
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return BadRequest(ModelState);
            }
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO login)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            User userModel = await userManager.FindByNameAsync(login.UserName);
            if (userModel != null)
            {
                if (await userManager.CheckPasswordAsync(userModel, login.Password) == true)
                {
                    //create token based on claims 
                    var claims = new List<Claim>();
                    claims.Add(new Claim(ClaimTypes.Name, userModel.UserName));
                    claims.Add(new Claim(ClaimTypes.NameIdentifier, userModel.Id));
                    var roles = await userManager.GetRolesAsync(userModel);
                    foreach (var item in roles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, item));
                    }
                    claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

                    ////put on token
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SecretKey"]));
                    var token = new JwtSecurityToken(
                        //body token
                        audience: configuration["JWT:ValidAudience"],
                        issuer: configuration["JWT:ValidIssuer"],
                        expires: DateTime.Now.AddDays(3),
                        claims: claims,
                        signingCredentials:
                        new SigningCredentials
                        (key, SecurityAlgorithms.HmacSha256)
                        );
                    return Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        expiration = token.ValidTo
                    });

                }
                return Unauthorized();
            }
            return BadRequest("User Name or Password Incorrect");
        }
        [HttpPost("RegisterAdmin")]
       // [Authorize(Roles = "Admin")]

        public async Task<IActionResult> RegisterAdmin(RegisterDTO register)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            User userModel = new User()
            {
                UserName = register.UserName,
                Email = register.Email,
            };
            IdentityResult result = await userManager.CreateAsync(userModel, register.Password);
            if (result.Succeeded)
            {
                //add to role manager
             IdentityResult resultRole=  await userManager.AddToRoleAsync(userModel,"Admin");
                if (resultRole.Succeeded)
                {
                    //await signInManager.SignInAsync(userModel, false) ;
                    return Ok(userModel);
                }
                else
                {
                    return BadRequest("error");
                }
              
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return BadRequest(ModelState);
            }
        }
    }
}
