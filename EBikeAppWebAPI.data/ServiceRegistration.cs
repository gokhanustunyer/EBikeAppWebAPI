using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using EBikeAppWebAPI.entity.Identity;
using EBikeAppWebAPI.data.Context;
using EBikeAppWebAPI.data.Abstract.Auth.Endpoint;
using EBikeAppWebAPI.data.Concrete.Auth.Endpoint;
using EBikeAppWebAPI.data.Concrete.Auth.Menu;

namespace EBikeAppWebAPI.data
{
    public static class ServiceRegistration
    {
        public static void AddDataServices(this IServiceCollection services, IConfiguration _configuration)
        {
            string connectionString = _configuration["ConnectionStrings:MySQL"];
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 29));
            services.AddDbContext<EBikeDbContext>(options => options.UseMySql(connectionString, serverVersion));


            services.AddScoped<IMenuRepository, MenuRepository>();
            services.AddScoped<IEndpointRepository, EndpointRepository>();
        }
    }
}
