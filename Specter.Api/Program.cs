﻿using System.Threading.Tasks;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using Specter.Api.Data;
using Specter.Api.Services;

namespace Specter.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            try
            {
                using (var scope = host.Services.CreateScope())
                {
                    var seeder = scope.ServiceProvider.GetRequiredService<ISeeder>();
                    var context = scope.ServiceProvider.GetRequiredService<SpecterDb>();

                    await context.Database.MigrateAsync();

                    await seeder.Seed();
#if DEBUG
                    var dataSeeder = scope.ServiceProvider.GetRequiredService<ITestDataSeeder>();

                    if (dataSeeder.CanSeed)
                        dataSeeder.SeedTestData();
#endif
                }
            }
            catch
            {

            }

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) => WebHost.CreateDefaultBuilder(args).UseStartup<Startup>();
    }
}
