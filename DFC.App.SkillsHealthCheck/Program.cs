﻿using System.Diagnostics.CodeAnalysis;

using DFC.Compui.Telemetry.HostExtensions;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace DFC.App.SkillsHealthCheck
{
    [ExcludeFromCodeCoverage]
    public static class Program
    {
        public static void Main(string[] args)
        {
            var webHost = CreateWebHostBuilder(args);

            webHost
                .Build()
                .AddApplicationTelemetryInitializer()
                .Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
