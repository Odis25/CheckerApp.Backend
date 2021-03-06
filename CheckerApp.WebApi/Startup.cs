using CheckerApp.Application;
using CheckerApp.Application.Interfaces;
using CheckerApp.Persistence;
using CheckerApp.WebApi.Common.JsonConverters;
using CheckerApp.WebApi.Common.Middleware;
using CheckerApp.WebApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Net.Http;

namespace CheckerApp.WebApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) =>
            Configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddPersistence(Configuration);
            services.AddApplication();
            services.AddControllers()
                .AddJsonOptions(config => 
                {
                    config.JsonSerializerOptions.WriteIndented = true;
                    config.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                    config.JsonSerializerOptions.Converters.Add(new HardwareConverter());
                    config.JsonSerializerOptions.Converters.Add(new SoftwareConverter());
                });

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.WithOrigins(
                        "https://pnrsu-server.incomsystem.ru:5000",
                        "https://localhost:5000",
                        "https://opnrdiso002.incomsystem.ru:5000",
                        "https://192.168.110.17:5000")
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });
            });

            services.AddAuthentication(config =>
            {
                config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = "https://pnrsu-server.incomsystem.ru:10001";
                    //options.Authority = "https://opnrdiso002.incomsystem.ru:10001";
                    options.Audience = "CheckerAPI";
                    options.RequireHttpsMetadata = true;
                    options.TokenValidationParameters.RoleClaimType = "checkerapp_role";
                });

            services.AddSingleton<ICurrentUserService, CurrentUserService>();
            services.AddHttpContextAccessor();
            services.AddHttpClient("AuthServer", config => 
                 config.BaseAddress = new Uri("https://pnrsu-server.incomsystem.ru:10001"));
                 //config.BaseAddress = new Uri("https://opnrdiso002.incomsystem.ru:10001"));
            services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("AuthServer"));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCustomExceptionHandler();
            app.UseRouting();
            app.UseHttpsRedirection();
            app.UseCors();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
