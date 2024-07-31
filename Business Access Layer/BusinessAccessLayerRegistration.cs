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
           // services.AddAuthentication(options =>
           // {
           //     options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
           //     options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
           // })
           //.AddJwtBearer(o =>
           //{
           //    o.RequireHttpsMetadata = false;
           //    o.SaveToken = false;
           //    o.TokenValidationParameters = new TokenValidationParameters
           //    {
           //        ValidateIssuerSigningKey = true,
           //        ValidateIssuer = true,
           //        ValidateAudience = true,
           //        ValidateLifetime = true,
           //        ValidIssuer = configuration["JWT:Issuer"],
           //        ValidAudience = configuration["JWT:Audience"],
           //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]))
           //    };
           //});
            return services;
        }

    }
}
