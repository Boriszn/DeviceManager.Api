using AutoMapper;
using DeviceManager.Api.ActionFilters;
using DeviceManager.Api.Configuration;
using DeviceManager.Api.Constants;
using DeviceManager.Api.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace DeviceManager.Api
{
    /// <summary>
    /// Configuration class for dotnet core application
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="env">The env.</param>
        /// <param name="configuration">Application configuration built in <see cref="Program"/></param>
        public Startup(IHostingEnvironment env, IConfiguration configuration)
        {
            _env = env;
            Configuration = configuration;
        }

        private IHostingEnvironment _env { get; }

        /// <summary>
        /// Instance of application configuration
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Configures the services.
        /// </summary>
        /// <param name="services">The services.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigurationOptions.ConfigureService(services, Configuration);

            // Add framework services.
            services.AddMvc(
                options =>
                {
                    options.Filters.Add(typeof(ValidateModelStateAttribute));
                });
            // Remove the commented code below and add before semicolon in the above line
            // if the assembly of the API Controllers is different than project which contains Startup class 
            //.AddApplicationPart(typeof(BaseController<>).Assembly);

            if (_env.IsProduction())
            {
                services.AddHsts(options =>
                {
                    options.Preload = true;
                    options.IncludeSubDomains = true;
                    options.MaxAge = TimeSpan.FromDays(60);
                });
            }
            // Localization support
            LocalizationConfiguration.ConfigureService(services);

            // Authentication using IdentityServer4
            AuthenticationConfiguration.Configure(services);

            Mapper.Reset();
            // https://github.com/AutoMapper/AutoMapper.Extensions.Microsoft.DependencyInjection/issues/28
            services.AddAutoMapper(typeof(Startup));

            // Swagger API documentation
            SwaggerConfiguration.ConfigureService(services);

            // IOC containers / Entity Framework
            EntityFrameworkConfiguration.ConfigureService(services, Configuration);
            IocContainerConfiguration.ConfigureService(services, Configuration);
            ApiVersioningConfiguration.ConfigureService(services);
        }

        /// <summary>
        /// Configures the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="env">The env.</param>
        /// <param name="loggerFactory">The logger factory.</param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (_env.IsProduction())
            {
                loggerFactory.AddFile(Configuration.GetSection(DefaultConstants.Logging));
                app.UseHsts();
                app.UseHttpsRedirection();
            }
            else
            {
                // Log in console only in development
                loggerFactory.AddConsole();
                loggerFactory.AddDebug();
            }

            // Localization support
            LocalizationConfiguration.Configure(app);

            // Authentication
            app.UseAuthentication();

            app.UseMiddleware<ExceptionHandlerMiddleware>();

            app.UseStaticFiles();

            //Cunfigure the Swagger API documentation
            SwaggerConfiguration.Configure(app);

            app.UseMvc();
        }
    }
}