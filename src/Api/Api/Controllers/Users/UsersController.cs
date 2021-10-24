using Api.Controllers.Users.Models;
using Api.Infrastructure;
using Api.Infrastructure.Constants;
using Api.Infrastructure.Extensions;
using Api.Services;
using AutoMapper;
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
        private readonly IMapper _mapper;

        public UsersController(UserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet("[action]")]
        //[ResponseCache(Duration = 5)]
        public async Task<UserResponse> Me()
        {
            var userId = User.GetUserId();
            var user = await _userService.GetById(userId);

            var response = _mapper.Map<UserResponse>(user);
            return response;
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> Update([FromBody] UpdateUserRequest model)
        {
            var userId = User.GetUserId();
            var updated = await _userService.Update(
                userId,
                model.GivenName,
                model.FamilyName,
                model.About);

            if (updated)
                return Ok();

            return BadRequest(ApiErrors.FAIL_UPDATE);
        }
    }
}
