using DataAccess.Context;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Models.IdentityModels;
using Repository.Core;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace SuppliersApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenService _tokenService; 
        public AuthController(IUnitOfWork unitOfWork, ITokenService tokenService)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
        }
        [HttpPost,Route("login")]
        public IActionResult Login([FromBody] LoginModel userLogin)
        {
            if(userLogin is null)
            {
                return BadRequest("Invalid Request");
            }

            var user = _unitOfWork.LoginModels.AuthenticateUser(userLogin.UserName, userLogin.Password);
            if(user == null)
                return Unauthorized();

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, userLogin.UserName),
                new Claim(ClaimTypes.Role, "User")
            };
            var accessToken = _tokenService.GenerateAccessToken(claims);
            var refreshToken = _tokenService.GenerateRefreshToken();
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpireTime = DateTime.Now.AddDays(7);

            _unitOfWork.LoginModels.Update(user);

            return Ok(new AuthenticatedResponse 
            { 
                Token = accessToken,
                RefreshToken = refreshToken
            });
            
        }
    }
}
