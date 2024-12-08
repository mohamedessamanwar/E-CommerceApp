using AspNetCoreRateLimit;
using BusinessAccessLayer;
using BusinessAccessLayer.Middleware;
using DataAccessLayer;
using DataAccessLayer.Data.Context;
using DataAccessLayer.Data.Models;
using E_CommerceApp.Fillter;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Stripe;
using System.Text;
namespace E_CommerceApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Configuration.AddJsonFile("Serlog.json");
            builder.Services.AddDataProtection();
            // Load configuration from appsettings.json
            var config = new ConfigurationBuilder()
                .AddJsonFile("Serlog.json")
                .Build();

            // Configure Serilog
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(config)
                .CreateLogger();

            // Ensure the application uses Serilog for logging
            builder.Host.UseSerilog(); 

            // Add services to the container.
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            #region CORS Policy
            builder.Services.AddCors(options =>
            {
                //options.AddPolicy("AllowAllDomains", policy =>
                //{
                //    policy.AllowAnyOrigin()
                //          .AllowAnyHeader()
                //          .AllowAnyMethod()
                //          .WithOrigins("");
                    
                //});
                options.AddPolicy("AllowAllDomains", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });
            #endregion
            builder.Services.DataAccessLayer(builder.Configuration);
            builder.Services.BusinessAccessLayer(builder.Configuration);
            builder.Services.AddScoped<FillterAction>(); // Register CustomActionFilter
            #region EF
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
               .AddEntityFrameworkStores<ECommerceContext>()
               .AddDefaultTokenProviders();
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
             .AddJwtBearer(o =>
             {
                 o.RequireHttpsMetadata = false;
                 o.SaveToken = false;
                 o.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuerSigningKey = true,
                     ValidateIssuer = true,
                     ValidateAudience = true,
                     ValidateLifetime = true,
                     ValidIssuer = builder.Configuration["JWT:Issuer"],
                     ValidAudience = builder.Configuration["JWT:Audience"],
                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
                 };
             });

            builder.Services.Configure<IdentityOptions>(options =>
            {
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1); // Lockout duration
                options.Lockout.MaxFailedAccessAttempts = 5; // Max attempts before lockout
                options.Lockout.AllowedForNewUsers = true; // Lockout new users
            });
            #endregion
            #region Rate Limit 
            //The above code registers the required services for rate limiting and configures the IP rate limiting options.
            //It uses an in-memory cache for storing rate limit counters.
            // Add services to the container
            builder.Services.AddMemoryCache();
            builder.Services.Configure<IpRateLimitOptions>(builder.Configuration.GetSection("IpRateLimiting"));
            builder.Services.Configure<IpRateLimitPolicies>(builder.Configuration.GetSection("IpRateLimitPolicies"));
            builder.Services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
            builder.Services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
            builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

            // Add the processing strategy
           // builder.Services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();
            #endregion
            #region Api Version 
            builder.Services.AddApiVersioning(options =>
    {
        options.AssumeDefaultVersionWhenUnspecified = true;
        options.DefaultApiVersion = new ApiVersion(1, 0); //default version 1.0
        options.ReportApiVersions = true; //add version info to response headers
        options.ApiVersionReader = ApiVersionReader.Combine(
            new UrlSegmentApiVersionReader(), //reads version from URL segment (e.g., /api/v1/)
            new QueryStringApiVersionReader("api-version"), //reads version from query string (e.g., /api/products?api-version=1.0)
            new HeaderApiVersionReader("X-Version"), //reads version from request header (e.g., X-Version: 1.0)
            new MediaTypeApiVersionReader("X-Version")); //reads version from the media type (e.g., application/json; version=1.0)
    }); 
            #endregion

            // builder.Services.AddLogging(); // Add logging services
            builder.Services.AddControllersWithViews(options =>
            {
                //can us DPI in this class .  
                options.Filters.Add<LogActivity>();
             //   options.Filters.Add<FillterAction>();  // Register as a global filter
            });

         //   builder.Services.AddScoped<FillterAction>(); // Register the LogActivity filter
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                //app.UseSwagger();
                //app.UseSwaggerUI();
            }
            app.UseCors("AllowAllDomains");
            app.UseMiddleware<ErrorHandlerMiddleware>();

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            //configuring the use of static files in an ASP.NET Core application

            //This line defines the path to the directory where the static files are located.
            var staticFilesPath = Path.Combine(Environment.CurrentDirectory, "wwwroot", "Images", "Product");
            //This line adds the middleware for serving static files to the application pipeline.
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(staticFilesPath),
                //This line sets the RequestPath property  .
                //The files in this directory will be accessible using the "/Images/Product" URL path .
                RequestPath = "/Images/Product"
            });
            // you can add another path to serve static files in addition to the existing path.
            // To do that, you can add another UseStaticFiles middleware configuration with
            // a different FileProvider and RequestPath.
            app.UseIpRateLimiting();

            app.MapControllers();
            app.Run();
            // serlog . 
            Log.CloseAndFlush();
        }
    }
}
