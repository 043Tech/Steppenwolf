using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Steppenwolf.Models;
using Steppenwolf.PostgresRepositories.Context;

namespace Steppenwolf.Extensions
{
    public static class HostExtensions
    {
        public static IHost SeedData(this IHost host)
        {
            using (var serviceScope = host.Services.CreateScope())
            {
                var serviceProvider = serviceScope.ServiceProvider;    
                
                // TODO Clean up
                var context = serviceProvider.GetService<PostgresDbContext>();
                if (!context.Tests.ToList().Any())
                {
                    context.Tests.AddRange(new[]
                    {
                        new Test() { Type = "Freezing" },
                        new Test() { Type = "Bracing" },
                        new Test() { Type = "Chilly" },
                        new Test() { Type = "Cool" },
                        new Test() { Type = "Mild" },
                        new Test() { Type = "Warm" },
                        new Test() { Type = "Balmy" },
                        new Test() { Type = "Hot" },
                        new Test() { Type = "Sweltering" },
                        new Test() { Type = "Scorching" },
                    });

                    context.SaveChanges();
                }

                var env = serviceProvider.GetService<IHostEnvironment>();
                if (env.IsDevelopment())
                {
                    var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();
                    const string admin = "Admin123@localhost";
                    if (userManager.FindByNameAsync(admin).Result == null)
                    {
                        var user = new ApplicationUser { UserName = admin, Email = admin };
                        var result = userManager.CreateAsync(user, admin).Result;
                    }
                }
            }

            return host;
        }
    }
}