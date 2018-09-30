using AutoMapper;
using DeviceManager.Api.ActionFilters;
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
    /// <summary>
    /// Configuration class for dotnet core application
    /// </summary>
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
        
        /// <summary>
        /// Instance of application configuration
        /// </summary>
        /// <value></value>
        public IConfigurationRoot Configuration { get; }

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
            // Remove commented code and above semicolon 
            // if the assembly of the API Controllers is different than project which contains Startup class 
            //.AddApplicationPart(typeof(BaseController<>).Assembly);

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
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            // loggerFactory.AddFile(Configuration.GetSection("Logging"));

            app.UseMiddleware<ExceptionHandlerMiddleware>();

            app.UseStaticFiles();

            //Cunfigure the Swagger API documentation
            SwaggerConfiguration.Configure(app);

            app.UseMvc();
        }
    }
}