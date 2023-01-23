using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using JWTTOKEN.Models;
using JWTTOKEN.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace JWTTOKEN.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IdentityController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;
        public IdentityController(IUserService userService, ITokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }

        [HttpPost("token")]
        [AllowAnonymous]
        public IActionResult Token([FromBody] LoginModel loginModel)
        {
            var user = _userService.AuthenticateUser(loginModel.Username, loginModel.Password);
            if (user == null)
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }

            Claim[] claims = _userService.GetClaims(user);
            var token = _tokenService.GenerateToken(claims);
            return Ok(token);
        }
    }
}

