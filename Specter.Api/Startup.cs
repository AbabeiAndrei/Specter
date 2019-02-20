using System;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;

using AutoMapper;

using Specter.Api.Data;
using Specter.Api.Mapper;
using Specter.Api.Services;
using Specter.Api.Data.Entities;
using Specter.Api.Data.Repository;
using Specter.Api.Services.Filtering;
using Specter.Api.Services.Email;
using Microsoft.Extensions.Logging;

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

            Action<DbContextOptionsBuilder> action;
            var si = new SecretInterpreter();

            if(Configuration.GetValue<bool>("UseInMemoryDb"))
                action = options => options.UseInMemoryDatabase(si.GetKey(Configuration.GetConnectionString("SpecterDbInMemory")));
            else
                action = options => options.UseSqlServer(si.GetKey(Configuration.GetConnectionString("SpecterDb")));

            services.AddDbContext<SpecterDb>(action);
            
            services.AddIdentity<ApplicationUser, IdentityRole<Guid>>(options =>
            {
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<SpecterDb>()
            .AddDefaultTokenProviders();

            services.AddLogging(ConfigureLogging);

            ConfigureAuthorization(services);

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
            
            app.UseStatusCodePages();
            app.UseAuthentication();
            app.UseMvc();
        }

        private void ConfigureAuthorization(IServiceCollection services)
        {
            var secret = Configuration.GetValue<string>("Secret");
            var key = Encoding.ASCII.GetBytes(secret);

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };

                x.Events = new JwtBearerEvents
                {
                    OnTokenValidated = o => 
                    {
                        return Task.CompletedTask;
                    },
                    OnMessageReceived = o => 
                    {
                        return Task.CompletedTask;
                    },
                    OnAuthenticationFailed = o =>
                    {
                        return Task.CompletedTask;
                    },
                    OnChallenge = o =>
                    {
                        return Task.CompletedTask;
                    }
                };
            });
        }

        private void ConfigureInjectedServices(IServiceCollection services)
        {
            services.AddScoped<IApplicationContext, SpecterDb>();
            services.AddScoped<ISeeder, SpecterSeeder>();
#if DEBUG
            services.AddScoped<ITestDataSeeder, TestDataSeeder>();
#endif

            services.AddScoped<IDateParserService, DateParserService>();
            services.AddScoped<IUrlCreatorService, UrlCreatorService>();
            services.AddScoped<IEmailTemplateBuilder, EmailTemplateBuilder>();
            services.AddScoped<IEmailService, SmtpEmailService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IReportingFilterService, ReportingFilterService>();            
            services.AddScoped<IFilterParser, FilterParser>();            

            services.AddScoped<ITimesheetRepository, TimesheetRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IDeliveryRepository, DeliveryRepository>();

            services.AddScoped<ITimesheetIdCalculator, TimesheetIdCalculator>();
            services.AddScoped<ISecretInterpreter, SecretInterpreter>();
            
            services.AddSingleton<IEmailConfiguration>(Configuration.GetSection("EmailConfig").Get<EmailConfiguration>());
            services.AddSingleton(CreateMapper());
            services.AddSingleton(FilterKeywordDictionary.Default());
        }
        
        private void ConfigureLogging(ILoggingBuilder builder)
        {
            builder.AddConsole();
            builder.AddDebug();
            builder.AddEventSourceLogger();
            builder.AddConfiguration(Configuration.GetSection("Logging"));
        }

        private IMapper CreateMapper()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new SpecterMappingProfile());
            });

            return  mappingConfig.CreateMapper();;
        }
    }
}
