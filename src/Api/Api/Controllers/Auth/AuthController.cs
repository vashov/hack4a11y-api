using Api.Controllers.Auth.Models;
using Api.Controllers.Users.Models;
using Api.Data.Entities;
using Api.Infrastructure;
using Api.Infrastructure.Constants;
using Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Api.Controllers.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserService _userService;

        public AuthController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> Register([FromBody] RegisterRequest model)
        {
            var existedUser = await _userService.GetByPhone(model.Login);
            if (existedUser != null)
            {
                return BadRequest(ApiErrors.ERROR_USER_EXISTS);
            }

            string role = MapRole(model.Role);
            if (role == null)
            {
                return BadRequest(ApiErrors.ERROR_ROLE);
            }

            var hash = new PasswordHasher().CreateHash(model.Password);
            var user = await _userService.Create(model.Login, hash, role);
            if (user == null)
            {
                return BadRequest(ApiErrors.ERROR_USER_NOT_CREATED);
            }

            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<LoginResponse>> Token([FromBody] LoginRequest model)
        {
            var user = await _userService.GetByPhone(model.Login);
            if (user == null || !new PasswordHasher().IsPasswordsEquals(model.Password, user.PasswordHash))
            {
                return BadRequest(ApiErrors.ERROR_INVALID);
            }

            string encodedJwt = GetEncodedToken(user);

            var response = new LoginResponse
            {
                AccessToken = encodedJwt,
                Username = user.PhoneNumber.ToString()
            };

            return response;
        }

        private static string GetEncodedToken(User user)
        {
            var now = DateTime.UtcNow;
            var signingCredentials = new SigningCredentials(
                AuthOptions.GetSymmetricSecurityKey(),
                SecurityAlgorithms.HmacSha256);

            var claims = GetClaims(user);

            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: signingCredentials);

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }

        private static List<Claim> GetClaims(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(CustomClaimTypes.Id, user.Id.ToString()),
                new Claim(CustomClaimTypes.Phone, user.PhoneNumber.ToString())
            };

            foreach (var role in user.Roles)
            {
                claims.Add(new Claim(CustomClaimTypes.Role, role.Name));
            }

            return claims;
        }

        private string MapRole(AwailableRoles role)
        {
            return role switch
            {
                AwailableRoles.Creator => Roles.Creator,
                AwailableRoles.Executor => Roles.Executor,
                _ => null
            };
        }
    }
}
