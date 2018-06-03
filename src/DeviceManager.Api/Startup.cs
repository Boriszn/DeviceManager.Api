using AutoMapper;
using DeviceManager.Api.Configuration;
using DeviceManager.Api.Middlewares;
using DeviceManager.Api.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DeviceManager.Api
{
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="env">The env.</param>
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        /// <summary>
        /// Configures the services.
        /// </summary>
        /// <param name="services">The services.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigurationOptions.ConfigureService(services, Configuration);

            // Add framework services.
            services.AddMvc();

            Mapper.Reset();
            services.AddAutoMapper();

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
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMiddleware<ExceptionHandlerMiddleware>();
            app.UseMvc();

            //Cunfigure the Swagger API documentation
            SwaggerConfiguration.Configure(app);
        }
    }
}