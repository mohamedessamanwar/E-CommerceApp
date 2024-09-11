using DataAccessLayer.Data.Context;
using DataAccessLayer.Data.Models;
using DataAccessLayer.Repositories.AddessRepo;
using DataAccessLayer.Repositories.OrderItemRepo;
using DataAccessLayer.Repositories.OrderRepo;
using DataAccessLayer.Repositories.ProductRepo;
using DataAccessLayer.Repositories.ShoppingCartRepo;
using DataAccessLayer.UnitOfWorkRepo;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data;

namespace DataAccessLayer
{
    public static class DataAccessLayerRegistration
    {

        public static IServiceCollection DataAccessLayer(this IServiceCollection services , IConfiguration configuration)
        {
            services.AddDbContext<ECommerceContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IAddressRepo, AddressRepo>();
            services.AddScoped<IShoppiingCartRepo,ShoppingCartRepo>();
            services.AddScoped<IOrderRepo, OrderRepo>();
            services.AddScoped<IOrderItemRepo, OrderItemRepo>();
            // Register Dapper-related services
            services.AddScoped<DapperContext>();
            return services;
        }


    }
}
