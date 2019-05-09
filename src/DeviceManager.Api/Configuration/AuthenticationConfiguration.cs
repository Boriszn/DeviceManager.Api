using DeviceManager.Api.Configuration.Settings;
using DeviceManager.Api.Constants;
using DeviceManager.Api.Helpers;
using IdentityModel;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace DeviceManager.Api.Configuration
{
    /// <summary>
    /// Oauth2 authentication settings
    /// </summary>
    public static class AuthenticationConfiguration
    {
        /// <summary>
        /// configures oauth2 authentication based on the configuration
        /// </summary>
        /// <param name="services">services</param>
        public static void Configure(IServiceCollection services)
        {
            // Get the configuration Settings
            // TODO: are there any alternative ways exist to get the configuration settings??
            var authenticationSettings = services.BuildServiceProvider().GetRequiredService<AuthenticationSettings>();

            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = GenericHelper.GetUriFromEnvironmentVariable(DefaultConstants.AuthenticationAuthority).ToString();
                    options.ApiName = authenticationSettings.Scope;
                    options.RequireHttpsMetadata = false;
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(PolicyConstants.Admin, policy =>
                {
                    // Allowed to access the resource if role admin or manager
                    policy.RequireClaim(JwtClaimTypes.Role, new[] { PolicyConstants.Admin, PolicyConstants.Manager });
                });

                options.AddPolicy(PolicyConstants.Manager, policy =>
                {
                    // Allowed only if the role of the logged in user is manager
                    policy.RequireClaim(JwtClaimTypes.Role, PolicyConstants.Manager);
                });

                var defaultPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser();

                defaultPolicy.RequireAssertion(c =>
                {
                    var mvcContext = c.Resource as Microsoft.AspNetCore.Mvc.Filters.AuthorizationFilterContext;
                    if (c.User.HasClaim(JwtClaimTypes.Role, PolicyConstants.Admin))
                        return true;

                    // Now the user needs to have tenant id
                    if (c.User.HasClaim(DefaultConstants.TenantClaim, mvcContext.HttpContext.Request.Headers[DefaultConstants.TenantId].ToString()))
                        return true;

                    return false;
                });

                options.DefaultPolicy = defaultPolicy.Build();
            });
        }
    }
}
