
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using DTOs.AutoMapperConfig;
using ShipAPI.Middleware;
using ShipAPI.Models;
using ShipAPI.Services;
using System.Text;
using BusinessLayer;

namespace ShipAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // builder.Logging.ClearProviders();
            //builder.Logging.AddDebug();
            //builder.Logging.AddConsole();

            /*
             * Serilog
             
    
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.File("Log\\log.txt",rollingInterval:RollingInterval.Minute)
                .CreateLogger();
            // This is to  add seriog provider
           // builder.Logging.AddSerilog();
           // builder.Services.AddSerilog();
            // this is for overriding built in provider
            builder.Host.UseSerilog();
            */

            // Log4Net
            builder.Logging.AddLog4Net();


            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
                options.AddPolicy("MyPolicy", options =>
                {

                });
            });

            // Add services to the container.
            builder.Services.AddControllers(options => options.ReturnHttpNotAcceptable = true).AddNewtonsoftJson().AddXmlDataContractSerializerFormatters();
            // builder.Services.AddSingleton<CUserService>();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.RegisterServices(builder.Configuration);

            // Automapper
            builder.Services.AddAutoMapper(typeof(AutoMapperConfigProfile));


            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(Options =>
            {
                // Options.RequireHttpsMetadata = false; // never make it false in production environment
                Options.SaveToken = true;
                Options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration.GetValue<string>("JWTSecretekey"))),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                };
            });
            var app = builder.Build();


            app.UseMiddleware<RequestLoggingMiddleware>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors();
            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
