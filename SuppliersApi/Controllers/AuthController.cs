using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Models.IdentityModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SuppliersApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel user)
        {
            if(user is null)
            {
                return BadRequest("Invalid Request");
            }
            if(user.UserName == "johnDoe" && user.Password == "def123")
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("sup£rpa55w0rd!"));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Role, "User")
                };

                var tokeOptions = new JwtSecurityToken(
                    issuer: "https://localhost:5001",
                    audience: "https://localhost:5001",
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(5),
                    signingCredentials: signinCredentials
                    );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);

                return Ok(new AuthenticatedResponse { Token = tokenString });
            }
            return Unauthorized();
        }
    }
}
