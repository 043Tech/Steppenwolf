using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Steppenwolf.Config;
using Steppenwolf.Extensions;
using Steppenwolf.Models;
using Steppenwolf.PostgresRepositories.Context;
using Steppenwolf.PostgresRepositories.Interfaces;
using Steppenwolf.PostgresRepositories.Repositories;
using Steppenwolf.Services;
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
            services.AddAutoMapper(typeof(Startup));
            services.AddRazorPages();
            services.AddDbContext<PostgresDbContext>(options => options.UseNpgsql(
                this.Configuration.GetConnectionString("PostgreSQL")
            ));
            var identityConfig = new IdentityProviders();
            this.Configuration.GetSection(nameof(IdentityProviders)).Bind(identityConfig);
            services.AddAuthentication(o =>
                {
                    o.DefaultScheme = IdentityConstants.ApplicationScheme;
                    o.DefaultSignInScheme = IdentityConstants.ExternalScheme;
                })
                .AddIdentityProviders(identityConfig)
                .AddIdentityCookies();

            services.AddIdentityCore<ApplicationUser>(o =>
                {
                    o.Stores.MaxLengthForKeys = 128;
                    o.SignIn.RequireConfirmedAccount = false;
                })
                .AddDefaultUI()
                .AddDefaultTokenProviders()
                .AddRoles<IdentityRole>()
                .AddRoleManager<RoleManager<IdentityRole>>()
                .AddEntityFrameworkStores<PostgresDbContext>();

            services.AddServerSideBlazor() // TODO change to client-side
                .AddCircuitOptions(options => { options.DetailedErrors = true; }); // TODO only for dev
            services.AddWebOptimizer(pipeline =>
            {
                pipeline.AddBundle(
                        "/css/style.css",
                        "text/css; charset=UTF-8",
                        "/wwwroot/scss/style.scss")
                    .UseContentRoot()
                    .CompileScss()
                    .Concatenate()
                    .FingerprintUrls()
                    .AddResponseHeader("X-Content-Type-Options", "nosniff")
                    .MinifyCss();
            });

            services.AddTransient<BlazorHelper>();
            services.AddTransient<BlogPostService>();

            // TODO Move to Api project
            services.AddSingleton<CategoryController>();
            services.AddTransient<BlogPostController>();
            services.AddTransient<IRepository<BlogPostEntity>, Repository<BlogPostEntity>>();
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

            app.UseSerilogRequestLogging();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseWebOptimizer();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}