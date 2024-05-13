using EBikeAppWebAPI.business.Abstract.Service;
using EBikeAppWebAPI.business.ViewModel.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EBikeAppWebAPI.Controllers
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

        [HttpPost("[action]")]
        public async Task<IActionResult> Register(UserCreateModel model)
        {
            bool isComplete = await _userService.CreateUser(model);
            return Ok(isComplete);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Login([FromBody] LoginUserModel model)
        {
            var response = await _userService.LoginAsync(model.Email, model.Password);
            return Ok(response);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> ConfirmEmail([FromQuery] string userId, [FromQuery] string token)
        {
            var response = await _userService.ConfirmEmail(userId, token);
            return Ok(response);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
        {
            var response = await _userService.ResetPasswordAsync(model);
            return Ok(response);
        }
    }
}
