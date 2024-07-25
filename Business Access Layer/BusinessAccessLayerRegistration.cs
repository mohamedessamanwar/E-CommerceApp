using Business_Access_Layer.Services.AuthService;
using BusinessAccessLayer.Services.AddressService;
using BusinessAccessLayer.Services.ProductService;
using BusinessAccessLayer.Services.ShoppingCartService;
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
            services.AddScoped<IAddressService, AddressService>();  
            services.AddScoped<IShoppingCartService,ShoppingCartService>();
            services.AddSingleton<Locke>();
            return services;
        }

    }
}
