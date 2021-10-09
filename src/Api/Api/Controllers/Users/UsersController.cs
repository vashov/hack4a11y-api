using Api.Controllers.Users.Models;
using Api.Infrastructure;
using Api.Infrastructure.Extensions;
using Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers.Users
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;

        public UsersController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet("/me")]
        [ResponseCache(Duration = 30)]
        public async Task<UserResponse> Me()
        {
            var userId = User.GetUserId();
            var user = await _userService.GetById(userId);

            var roles = user.Roles.Select(r => r.Name).ToList();

            var response = new UserResponse
            {
                Id = user.Id,
                Phone = user.PhoneNumber,
                Roles = roles
            };
            return response;
        }
    }
}
