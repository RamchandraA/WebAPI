using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WebAPI.Models;

namespace WebAPI
{
    public class Startup
    {
        private IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<WebAPIDbContext> (cfg =>
            {
                cfg.UseSqlServer(Configuration["ConnectionStrings:WebApiConnection"], sqlServerOptionsAction: sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(maxRetryCount:5, maxRetryDelay:TimeSpan.FromSeconds(15), errorNumbersToAdd: null);
                });
                if(Configuration["EnableEFLogging"]== "true")
                {
                    cfg.UseLoggerFactory(LoggerFactory.Create(lg => lg.AddConsole())).EnableSensitiveDataLogging();
                }
                if(Configuration["EnableEFLazyLoading"]== "true")
                {
                    cfg.UseLazyLoadingProxies(true);
                }
            });
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }
    }
}
