using DeviceManager.Api.Configuration.Settings;
using DeviceManager.Api.Constants;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Linq;

namespace DeviceManager.Api.ActionFilters
{
    /// <summary>
    /// Returns http response code along with descriptive message to the client when authentication or authorization is failed
    /// </summary>
    public class AuthorizeCheckOperationFilter : IOperationFilter
    {
        private AuthenticationSettings _authenticationSettings;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="authenticationSettings"></param>
        public AuthorizeCheckOperationFilter(AuthenticationSettings authenticationSettings)
        {
            _authenticationSettings = authenticationSettings;
        }

        /// <summary>
        /// Checks for non authorized requests and returns appropriate status codes
        /// </summary>
        /// <param name="operation">Current swagger request</param>
        /// <param name="context">Request context</param>
        public void Apply(Operation operation, OperationFilterContext context)
        {
            var hasAuthorize = context.ControllerActionDescriptor.GetControllerAndActionAttributes(true).OfType<AuthorizeAttribute>().Any();

            if (hasAuthorize)
            {
                operation.Responses.Add("401", new Response { Description = "Unauthorized" });
                operation.Responses.Add("403", new Response { Description = "Forbidden" });

                operation.Security = new List<IDictionary<string, IEnumerable<string>>>
                {
                    new Dictionary<string, IEnumerable<string>> {{ DefaultConstants.OAuth2 , new[] { _authenticationSettings.Scope } }}
                };
            }
        }
    }
}
