using System.IO;
using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using DeviceManager.Api.ActionFilters;
using DeviceManager.Api.Controllers;

namespace DeviceManager.Api.Configuration
{
    /// <summary>
    /// Swagger API documentation components start-up configuration
    /// </summary>
    public static class SwaggerConfiguration
    {
        /// <summary>
        /// Configures the service.
        /// </summary>
        /// <param name="services">The services.</param>
        public static void ConfigureService(IServiceCollection services)
        {
            // Swagger API documentation
            services.AddSwaggerGen(c =>
            {
                // TODO: Need to push hardcoded strings to resource file
                c.SwaggerDoc("v1",
                new Info
                {
                    Title = "Device Api",
                    Version = "v1.0",
                    Description = "Dotnet core multi tenant application",
                    TermsOfService = "TODO: Add Terms of service",
                    Contact = new Contact {
                        Name = "Boris Zaikin",
                        Email = "TODO: Add Contact email",
                        Url = "https://github.com/Boriszn/DeviceManager.Api"
                    },
                    License = new License {
                        Name = "MIT License",
                        Url = "https://opensource.org/licenses/MIT"
                    }
                });
                c.OperationFilter<TenantHeaderOperationFilter>();
                c.OperationFilter<LocalizationQueryOperationFilter>();
                c.SchemaFilter<SwaggerExcludeFilter>();
                var xmlFile = $"{typeof(BaseController<>).Assembly.GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        /// <summary>
        /// Configures the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        public static void Configure(IApplicationBuilder app)
        {
            
            // This will redirect default url to Swagger url
            var option = new RewriteOptions();
            option.AddRedirect("^$", "swagger");
            app.UseRewriter(option);

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Device Api v1.0");
            });
        }
    }
}