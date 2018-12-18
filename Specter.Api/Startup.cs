using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Specter.Api.Data;
using Specter.Api.Services;
using Specter.Api.Data.Entities;

namespace Specter.Api
{
    public class Startup
    {
        private readonly IHostingEnvironment _environment;

        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            Configuration = configuration;
            _environment = environment;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            if (_environment.IsDevelopment())
                services.AddDbContext<SpecterDb>(options => options.UseSqlite(Configuration.GetConnectionString("SpecterDbLite")));
            else
                services.AddDbContext<SpecterDb>(options => options.UseSqlServer(Configuration.GetConnectionString("SpecterDb")));

            ConfigureInjectedServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseCors(options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            }
            else
            {
                app.UseHsts();
                app.UseHttpsRedirection();
            }

            app.UseMvc();
        }

        public void ConfigureInjectedServices(IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = false;
            })
            .AddEntityFrameworkStores<SpecterDb>()
            .AddDefaultTokenProviders();

            services.AddScoped<IApplicationContext, SpecterDb>();
            services.AddScoped<ISeeder, SpecterSeeder>();
        }
    }
}
