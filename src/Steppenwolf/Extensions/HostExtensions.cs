using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Steppenwolf.Auth;
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
            var roleManager = context.GetService<RoleManager<IdentityRole>>();
            if (!roleManager.RoleExistsAsync(Roles.Blogger).Result)
            {
                roleManager.CreateAsync(new IdentityRole()
                {
                    Name = Roles.Blogger
                }).Wait();
            }
            
            if (env.IsDevelopment())
            {
                var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();
                const string admin = "Admin123@localhost";
                if (userManager.FindByNameAsync(admin).Result == null)
                {
                    var user = new ApplicationUser { UserName = admin, Email = admin };
                    var result = userManager.CreateAsync(user, admin).Result;
                    if (result.Succeeded)
                    {
                        userManager.AddToRoleAsync(user, Roles.Blogger).Wait();
                    }

                    // TODO Clean up
                    for (int i = 0; i < 20; i++)
                    {
                        var body = @"This blog post shows a few different types of content that’s supported and styled with Bootstrap. Basic typography, images, and code are all supported.

Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Aenean eu leo quam. Pellentesque ornare sem lacinia quam venenatis vestibulum. Sed posuere consectetur est at lobortis. Cras mattis consectetur purus sit amet fermentum.

Curabitur blandit tempus porttitor. Nullam quis risus eget urna mollis ornare vel eu leo. Nullam id dolor id nibh ultricies vehicula ut id elit.

Etiam porta sem malesuada magna mollis euismod. Cras mattis consectetur purus sit amet fermentum. Aenean lacinia bibendum nulla sed consectetur.

Heading
Vivamus sagittis lacus vel augue laoreet rutrum faucibus dolor auctor. Duis mollis, est non commodo luctus, nisi erat porttitor ligula, eget lacinia odio sem nec elit. Morbi leo risus, porta ac consectetur ac, vestibulum at eros.

Sub-heading
Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus.

Example code block
Aenean lacinia bibendum nulla sed consectetur. Etiam porta sem malesuada magna mollis euismod. Fusce dapibus, tellus ac cursus commodo, tortor mauris condimentum nibh, ut fermentum massa.

Sub-heading
Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Aenean lacinia bibendum nulla sed consectetur. Etiam porta sem malesuada magna mollis euismod. Fusce dapibus, tellus ac cursus commodo, tortor mauris condimentum nibh, ut fermentum massa justo sit amet risus.

Praesent commodo cursus magna, vel scelerisque nisl consectetur et.
Donec id elit non mi porta gravida at eget metus.
Nulla vitae elit libero, a pharetra augue.
Donec ullamcorper nulla non metus auctor fringilla. Nulla vitae elit libero, a pharetra augue.

Vestibulum id ligula porta felis euismod semper.
Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus.
Maecenas sed diam eget risus varius blandit sit amet non magna.
Cras mattis consectetur purus sit amet fermentum. Sed posuere consectetur est at lobortis.";
                        
                        context.Blogs.AddAsync(new BlogPostEntity() { Title = i + ".Sample blog post", AuthorId = user.Id, Body = body });
                    }

                    context.SaveChanges();
                }
            }

            return host;
        }
    }
}