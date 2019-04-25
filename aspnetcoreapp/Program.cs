using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace aspnetcoreapp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        // TODO :: Test Convenience methods ConfigureServices, Configure

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            // 1. Call CreateWebHostBuilder without params
            // WebHost.CreateDefaultBuilder()
            WebHost.CreateDefaultBuilder(args)
                // 2. Change Startup class name
                // .UseStartup<Startup2>();
                .UseStartup<Startup>();
    }
}
