using Application.Services.UserService;
using Application.UseCases.User;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
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
    }
}
