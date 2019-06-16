using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PaymentSolution.Common;
using PaymentSolution.Core.DTO;
using PaymentSolution.Core.Entity;
using PaymentSolution.ViewModel;

namespace PaymentSolution.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly AppSettings _setting;
        private readonly UserManager<AppUser> _userManager;
             
        public AccountController(UserManager<AppUser> userManager, IOptions<AppSettings> setting)
        {
            _userManager = userManager;
            _setting = setting.Value;
        }


       [Route("register")]
        [HttpPost]
        public async Task<ActionResult> Register ([FromForm] AppUserDTO model)
        {
            var user = new AppUser
            {
                Email = model.Email,
                UserName = model.Email,
                Fullname = model.FullName,
               

            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Customer");
            }
           // return Ok(new { Username = user.UserName });
            return CreatedAtAction("Login", new { Username = user.UserName }, model);
        }
        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromForm] LoginViewModel model)
            {

            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                //Get role assigned to the user
                var role = await _userManager.GetRolesAsync(user);
                IdentityOptions _options = new IdentityOptions();
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_setting.JWT_Secret));
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("UserID",user.Id.ToString()),
                        new Claim(ClaimTypes.Role, role.FirstOrDefault())
                    }),
                    
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);
                return Ok(new { token });
            }
            else
                return BadRequest(new { message = "Username or password is incorrect." });

        }
        }
    }
