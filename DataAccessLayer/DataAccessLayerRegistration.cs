using DataAccessLayer.Data.Context;
using DataAccessLayer.Repositories.AddessRepo;
using DataAccessLayer.Repositories.ProductRepo;
using DataAccessLayer.Repositories.ShoppingCartRepo;
using DataAccessLayer.UnitOfWorkRepo;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccessLayer
{
    public static class DataAccessLayerRegistration
    {

        public static IServiceCollection DataAccessLayer(this IServiceCollection services)
        {
            services.AddDbContext<ECommerceContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IAddressRepo, AddressRepo>();
           services.AddScoped<IShoppiingCartRepo,ShoppingCartRepo>();
            return services;
        }


    }
}
