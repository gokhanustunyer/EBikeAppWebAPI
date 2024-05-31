using EBikeAppWebAPI.business.Abstract.Service;
using EBikeAppWebAPI.business.Abstract.Storage;
using EBikeAppWebAPI.business.Operations.Statics;
using EBikeAppWebAPI.business.ServiceResponses.Bike;
using EBikeAppWebAPI.business.ViewModel.Bike;
using EBikeAppWebAPI.business.ViewModel.Location;
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
            double user_lat = location.Lat * Math.PI / 180;
            double user_long = location.Long * Math.PI / 180;

            foreach(Bike bike in _bikeRepository.GetAll())
            {
                double bike_lat = bike.Lat * Math.PI / 180;
                double bike_long = bike.Long * Math.PI / 180;

                double deltaLat = bike_lat - user_lat;
                double deltaLon = bike_long - user_long;

                double a = Math.Sin(deltaLat / 2) * Math.Sin(deltaLat / 2) + 
                           Math.Cos(user_lat) * Math.Cos(bike_lat) *
                           Math.Sin(deltaLon / 2) * Math.Sin(deltaLon / 2);
                double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

                double distance = 6371 * c;
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
    }
}
