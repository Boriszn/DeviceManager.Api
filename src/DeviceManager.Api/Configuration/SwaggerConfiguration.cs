using DeviceManager.Api.ActionFilters;
using DeviceManager.Api.Configuration.Settings;
using DeviceManager.Api.Constants;
using DeviceManager.Api.Controllers;
using DeviceManager.Api.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.IO;

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
            // Get configuration instance
            var authenticationSettings = services.BuildServiceProvider().GetRequiredService<AuthenticationSettings>();

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
                    Contact = new Contact
                    {
                        Name = "Boris Zaikin",
                        Email = "TODO: Add Contact email",
                        Url = "https://github.com/Boriszn/DeviceManager.Api"
                    },
                    License = new License
                    {
                        Name = "MIT License",
                        Url = "https://opensource.org/licenses/MIT"
                    }
                });

                c.OperationFilter<TenantHeaderOperationFilter>();
                c.OperationFilter<LocalizationQueryOperationFilter>();
                c.OperationFilter<AuthorizeCheckOperationFilter>(authenticationSettings);

                c.SchemaFilter<SwaggerExcludeFilter>();

                var xmlFile = $"{typeof(BaseController<>).Assembly.GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

                #region Identity Server Authentication

                var authenticationServerUri = GenericHelper.GetUriFromEnvironmentVariable(DefaultConstants.AuthenticationAuthority);
                var authorizationUri = GenericHelper.CombineUri(authenticationServerUri, authenticationSettings.AuthorizationUrl);

                c.AddSecurityDefinition(DefaultConstants.OAuth2, new OAuth2Scheme
                {
                    Flow = IdentityModel.OidcConstants.GrantTypes.Implicit,
                    AuthorizationUrl = authorizationUri.ToString(), 
                    Scopes = new Dictionary<string, string> { { authenticationSettings.Scope, DefaultConstants.ApiDescription } }
                });
                #endregion

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
                c.OAuthClientId("DeviceManagerApi_Swagger");
                c.OAuthAppName("Device Api Swagger Ui");
            });
        }
    }
}