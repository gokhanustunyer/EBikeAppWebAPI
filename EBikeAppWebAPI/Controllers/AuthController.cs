using EBikeAppWebAPI.business.Abstract.Service;
using EBikeAppWebAPI.business.ViewModel.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EBikeAppWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> ConfirmEmail([FromQuery] string userId, [FromQuery] string token)
        {
            bool response = await _userService.ConfirmEmail(userId, token);
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
