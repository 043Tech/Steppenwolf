using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Steppenwolf.CosmosRepositories.Context;
using Steppenwolf.CosmosRepositories.Interfaces;
using Steppenwolf.CosmosRepositories.Repositories;
using Steppenwolf.Models;
using Steppenwolf.Services.Data;

namespace Steppenwolf
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddDbContext<CosmosDbContext>(options => options.UseCosmos(
                this.Configuration["Cosmos:AccountEndpoint"],
                this.Configuration["Cosmos:AccountKey"],
                this.Configuration["Cosmos:AccountDatabase"]
                ));
            services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<CosmosDbContext>();
            
            services.AddServerSideBlazor(); // TODO change to client-side
            services.AddWebOptimizer(pipeline =>
            {
                pipeline.AddScssBundle("/css/style.css", "/scss/style.scss").UseContentRoot();
            });
            
            services.AddSingleton<CategoryService>();
            services.AddTransient<WeatherForecastService>();
            services.AddTransient<IRepository<Test>, Repository<Test>>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }
            
            app.UseWebOptimizer();

            app.UseSerilogRequestLogging();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}