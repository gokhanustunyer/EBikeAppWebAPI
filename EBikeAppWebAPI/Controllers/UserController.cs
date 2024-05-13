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

        [HttpPost("action")]
        public async Task<IActionResult> Register(UserCreateModel model)
        {
            bool isComplete = await _userService.CreateUser(model);
            return Ok(isComplete);
        }
    }
}
