using BusinessAccessLayer.Services.AuthService;
using BusinessAccessLayer.DTOS;
using BusinessAccessLayer.Services.AddressService;
using BusinessAccessLayer.Services.Email;
using BusinessAccessLayer.Services.OrderService;
using BusinessAccessLayer.Services.PaymentService;
using BusinessAccessLayer.Services.ProductService;
using BusinessAccessLayer.Services.ShoppingCartService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;
using BusinessAccessLayer.Services.Reviewervice;
using BusinessAccessLayer.Profiles;
using BusinessAccessLayer.Validations.ReviewValidation;
using FluentValidation;
using BusinessAccessLayer.DTOS.ReviewDtos;
using BusinessAccessLayer.Services.CacheService;
namespace BusinessAccessLayer
{
    public static class BusinessAccessLayerRegistration
    {
        

 
        public static IServiceCollection BusinessAccessLayer(this IServiceCollection services , IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddTransient<IMailingService, MailingService>();
            services.Configure<MailSetting>(configuration.GetSection("MailSetting"));
            services.Configure<JWT>(configuration.GetSection("JWT"));
            services.Configure<StripeSitting>(configuration.GetSection(nameof(Stripe)));
            services.AddScoped<IProductServices, ProductService>();
            services.AddScoped<IAuth, AuthService>();
            services.AddScoped<IAddressService, AddressService>();  
            services.AddScoped<IShoppingCartService,ShoppingCartService>();
            services.AddScoped<IOrderService, OrderService>();  
            services.AddSingleton<Locke>();
            services.AddScoped<IPayment,StripPayment>();
            services.AddScoped<IReviewService,ReviewService>();
            // Register AutoMapper and all profiles in the assembly
            services.AddAutoMapper(typeof(ReviewProfile));
            // Register the CreateReview class for DI
            services.AddTransient<IValidator<AddReview>, CreateReview>();
            services.AddSingleton<ICacheService, CacheService>();
            return services;
        }

    }
}
