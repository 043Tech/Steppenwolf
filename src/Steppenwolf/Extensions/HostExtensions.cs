using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Steppenwolf.CosmosRepositories.Context;
using Steppenwolf.Models;

namespace Steppenwolf.Extensions
{
    public static class HostExtensions
    {
        public static IHost SeedData(this IHost host)
        {
            using (var serviceScope = host.Services.CreateScope())
            {
                // TODO Move to repo when needed. This is for tests
                var context = serviceScope.ServiceProvider.GetService<CosmosDbContext>();
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
            }

            return host;
        }
    }
}