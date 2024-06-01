using EBikeAppWebAPI.business.Abstract.Service;
using EBikeAppWebAPI.business.Abstract.Storage;
using EBikeAppWebAPI.business.Operations.Statics;
using EBikeAppWebAPI.business.ServiceResponses.Bike;
using EBikeAppWebAPI.business.ServiceResponses.Ride;
using EBikeAppWebAPI.business.ViewModel.Bike;
using EBikeAppWebAPI.business.ViewModel.Location;
using EBikeAppWebAPI.business.ViewModel.Ride;
using EBikeAppWebAPI.data.Abstract.Bike;
using EBikeAppWebAPI.data.Context;
using EBikeAppWebAPI.entity.Bike;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBikeAppWebAPI.business.Concrete.Service
{
    public class BikeService : IBikeService
    {
        private readonly IBikeRepository _bikeRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILocalStorage _localStorage;
        private readonly IUserService _userService;
        private readonly EBikeDbContext _eBikeDbContext;
        readonly IHostEnvironment _webHostEnvironment;
        public BikeService(IBikeRepository bikeRepository,
                           IHttpContextAccessor httpContextAccessor,
                           EBikeDbContext eBikeDbContext,
                           IUserService userService,
                           ILocalStorage localStorage,
                           IHostEnvironment webHostEnvironment)
        {
            _bikeRepository = bikeRepository;
            _httpContextAccessor = httpContextAccessor;
            _eBikeDbContext = eBikeDbContext;
            _userService = userService;
            _localStorage = localStorage;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<CreateBikeResponse> CreateBikeAsync(CreateBikeModel createBikeModel)
        {
            Guid bikeId = Guid.NewGuid();
            var bikeImage = await _localStorage.UploadAsync("bikes", createBikeModel.Image);

            var qrCode = QrCodeGenerator.GenerateQrCode(bikeId.ToString(), Path.Combine(_webHostEnvironment.ContentRootPath, "wwwroot", "bikeQrs"));

            await _bikeRepository.AddAsync(new entity.Bike.Bike()
            {
                Id = bikeId,
                Image = bikeImage.path,
                Lat = createBikeModel.Lat,
                Long = createBikeModel.Long,
                PricePerMin = createBikeModel.PricePerMin,
                QrCode = qrCode.Item2,
                Token = qrCode.Item1
            });
            await _bikeRepository.SaveAsync();

            return new()
            {
                BikeToken = qrCode.Item1,
                QrCodePath = qrCode.Item2
            };
        }

        public List<GetCloseBikeByGeoLocationResponse> GetCloseBikeByGeoLocation(Location location)
        {
            List<GetCloseBikeByGeoLocationResponse> bikes = new();

            foreach(Bike bike in _bikeRepository.GetAll())
            {
                Location bikeLocation = new Location()
                {
                    Lat = bike.Lat,
                    Long = bike.Long,
                };
                double distance = GeoLocationCalculator.CalculateDistanceBetweenTwoPoint(location, bikeLocation);
                if (distance < 0.75)
                {
                    bikes.Add(new()
                    {
                        Id = bike.Id,
                        Image = bike.Image,
                        Lat = bike.Lat,
                        Long = bike.Long,
                        PricePerMin = bike.PricePerMin
                    });
                }
            }
            return bikes;
        }

        public Task<StartRideResponse> StartRide(StartRideModel startRideModel)
        {
            throw new NotImplementedException();
        }
    }
}
