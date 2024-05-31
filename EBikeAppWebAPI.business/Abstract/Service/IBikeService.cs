using EBikeAppWebAPI.business.ServiceResponses.Bike;
using EBikeAppWebAPI.business.ServiceResponses.Ride;
using EBikeAppWebAPI.business.ViewModel.Bike;
using EBikeAppWebAPI.business.ViewModel.Location;
using EBikeAppWebAPI.business.ViewModel.Ride;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBikeAppWebAPI.business.Abstract.Service
{
    public interface IBikeService
    {
        Task<CreateBikeResponse> CreateBikeAsync(CreateBikeModel createBikeModel);
        List<GetCloseBikeByGeoLocationResponse> GetCloseBikeByGeoLocation(Location location);
        Task<StartRideResponse> StartRide(StartRideModel startRideModel);
    }
}
