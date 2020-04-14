using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Debugging;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;

namespace Steppenwolf
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog((hostingContext, loggerConfiguration) =>
                {
                    loggerConfiguration
                        .MinimumLevel.Error()
                        .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                        .MinimumLevel.Override("Quartz", LogEventLevel.Warning)
                        .Enrich.FromLogContext()
                        .Enrich.WithProperty("Environment", hostingContext.HostingEnvironment)
                        .Enrich.WithProperty("HostName", Environment.MachineName)
                        .WriteTo.Console(theme: SystemConsoleTheme.Literate);

                    if (hostingContext.HostingEnvironment.IsDevelopment())
                    {
                        loggerConfiguration
                            .MinimumLevel.Verbose()
                            .WriteTo.File("./errorlogs.txt", LogEventLevel.Error);
                    }
                    
                    SelfLog.Enable(Console.Error);
                })
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}