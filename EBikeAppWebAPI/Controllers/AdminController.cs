using EBikeAppWebAPI.business.Abstract.Service;
using EBikeAppWebAPI.business.ViewModel.Bike;
using EBikeAppWebAPI.business.ViewModel.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EBikeAppWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IBikeService _bikeService;
        public AdminController(IBikeService bikeService)
        {
            _bikeService = bikeService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateBike([FromForm] CreateBikeModel model)
        {
            var response = await _bikeService.CreateBikeAsync(model);
            return Ok(response);
        }
    }
}
