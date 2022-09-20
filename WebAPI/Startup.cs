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
using WebAPI.Models.Abstract;
using WebAPI.Models.Concrete;

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
            services.AddMvc();

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

            services.AddScoped<IProductRepository, SqlProductRepository>();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();
            }

            using(var scope = app.ApplicationServices.CreateScope())
            {
                WebAPIDbContext context = scope.ServiceProvider.GetRequiredService<WebAPIDbContext>();
                var createDatabase = context.Database.EnsureCreated();
                if(createDatabase)
                {
                    ProductSeedData.PopulateWebApiData(context);
                    logger.LogInformation($"--- ProductSeedData called, '{context.Products.Count()}' - Product Added ---");
                }
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }
    }
}
