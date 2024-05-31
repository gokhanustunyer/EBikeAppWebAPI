﻿using EBikeAppWebAPI.business.Abstract.Handler;
using EBikeAppWebAPI.business.Abstract.Service;
using EBikeAppWebAPI.business.Abstract.Storage;
using EBikeAppWebAPI.business.Concrete.Handler;
using EBikeAppWebAPI.business.Concrete.Service;
using EBikeAppWebAPI.business.Concrete.Storage;
using EBikeAppWebAPI.data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EBikeAppWebAPI.business
{
    public static class ServiceRegistration
    {
        public static void AddBusinessServices(this IServiceCollection services, IConfiguration _configuration)
        {
            services.AddDataServices(_configuration);
            services.AddScoped<ILocalStorage, LocalStorage>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITokenHandler, TokenHandler>();
            services.AddScoped<IBikeService, BikeService>();
        }
    }
}
