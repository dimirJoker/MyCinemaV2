using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace MyCinemaV2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //var cultureInfo = new CultureInfo("en-US");
            //CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            //CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
