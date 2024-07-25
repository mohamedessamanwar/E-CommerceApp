using BusinessAccessLayer;
using BusinessAccessLayer.DTOS;
using BusinessAccessLayer.Middleware;
using BusinessAccessLayer.Services.Email;
using DataAccessLayer;
using DataAccessLayer.Data.Context;
using DataAccessLayer.Data.Models;
using E_CommerceApp.Fillter;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.IO;
using System.Text;
namespace E_CommerceApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Load configuration from appsettings.json
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            // Configure Serilog
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(config)
                .CreateLogger();

            // Ensure the application uses Serilog for logging
            builder.Host.UseSerilog();

            Log.Information("Application Starting");

            // Add services to the container.
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            #region CORS Policy
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllDomains", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });
            #endregion
            builder.Services.DataAccessLayer();
            builder.Services.BusinessAccessLayer();
            builder.Services.Configure<JWT>(builder.Configuration.GetSection("JWT"));
            builder.Services.Configure<MailSetting>(builder.Configuration.GetSection("MailSetting"));
            builder.Services.AddTransient<IMailingService, MailingService>();
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
           
           // builder.Services.AddLogging(); // Add logging services
            builder.Services.AddControllersWithViews(options =>
            {
                //can us DPI in this class .  
                options.Filters.Add<LogActivity>();
            });

            builder.Services.AddScoped<LogActivity>(); // Register the LogActivity filter
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
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


            app.MapControllers();
            app.Run();
            Log.CloseAndFlush();
        }
    }
}
