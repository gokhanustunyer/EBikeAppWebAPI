using EBikeAppWebAPI.business.Abstract.Service;
using EBikeAppWebAPI.business.CustomAttributes;
using EBikeAppWebAPI.business.Enums;
using EBikeAppWebAPI.business.ViewModel.Location;
using EBikeAppWebAPI.business.ViewModel.Ride;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EBikeAppWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RideController : ControllerBase
    {
        private readonly IBikeService _bikeService;

        public RideController(IBikeService bikeService)
        {
            _bikeService = bikeService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetCloseBikesByGeoLocation([FromQuery] Location location)
        {
            var response = _bikeService.GetCloseBikeByGeoLocation(location);
            return Ok(response);
        }
        [HttpPost("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(ActionType = ActionType.Writing, Definition = "Start Ride", Menu = "Ride")]
        public async Task<IActionResult> StartRide([FromBody] StartRideModel model)
        {
            // Check this lines
            if (!User.Identity.IsAuthenticated) { return Ok(); }
            model.UserName = User.Identity.Name;
            var response = _bikeService.StartRide(model);
            return Ok(response);
        }
    }
}
