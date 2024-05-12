using Business_Access_Layer.Services.AuthService;
using BusinessAccessLayer.Services.ProductService;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
namespace BusinessAccessLayer
{
    public static class BusinessAccessLayerRegistration
    {
        public static IServiceCollection BusinessAccessLayer(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<IProductServices, ProductService>();
            services.AddScoped<IAuth, AuthService>();
            return services;
        }

    }
}
