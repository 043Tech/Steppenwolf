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
            using var serviceScope = host.Services.CreateScope();
            var serviceProvider = serviceScope.ServiceProvider;
            var context = serviceProvider.GetService<PostgresDbContext>();

            var env = serviceProvider.GetService<IHostEnvironment>();
            if (env.IsDevelopment())
            {
                var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();
                const string admin = "Admin123@localhost";
                if (userManager.FindByNameAsync(admin).Result == null)
                {
                    var user = new ApplicationUser { UserName = admin, Email = admin };
                    var result = userManager.CreateAsync(user, admin).Result;

                    // TODO Clean up
                    var body = string.Empty;
                    for (int i = 0; i < 300; i++)
                    {
                        body += "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nam in lobortis eros. Nam gravida purus et interdum ullamcorper. ";
                    }

                    context.Blogs.AddAsync(new BlogPostEntity() { Title = "Freezing", AuthorId = user.Id, Body = body });

                    context.SaveChanges();
                }
            }

            return host;
        }
    }
}