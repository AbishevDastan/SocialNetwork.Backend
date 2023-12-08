using Application.Extensions.UserContext;
using Application.Services.UserService;
using Application.UseCases.User;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IUserContextService _userContextService;

        public UserController(IUserService userService, IUserContextService userContextService)
        {
            _userService = userService;
            _userContextService = userContextService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult<int>> Register(RegisterUserDto request)
        {
            var response = await _userService.Register(new User { Email = request.Email }, request.Password);
            return Ok(response);
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<TokenModel>> Login(AuthenticateUserDto request)
        {
            var response = await _userService.Login(request.Email, request.Password);
            return Ok(response);
        }

        [HttpPost("{followingId}/follow")]
        [Authorize]
        public async Task<ActionResult> FollowUser(int followingId)
        {
            var result = await _userService.FollowUser(_userContextService.GetCurrentUserId(), followingId);
            return Ok(result);
        }

        [HttpPost("{followingId}/unfollow")]
        [Authorize]
        public async Task<ActionResult> UnfollowUser(int followingId)
        {
            var result = await _userService.UnfollowUser(_userContextService.GetCurrentUserId(), followingId);
            return Ok(result);
        }
    }
}
