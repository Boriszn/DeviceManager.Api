using System.Collections.Generic;
using DeviceManager.Api.Helpers;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace DeviceManager.Api.ActionFilters
{
    /// <summary>
    /// Adds Tenant Id field to API endpoints
    /// </summary>
    /// <seealso cref="Swashbuckle.AspNetCore.SwaggerGen.IOperationFilter" />
    public class TenantHeaderOperationFilter : IOperationFilter
    {
        /// <summary>
        /// Applies the specified operation.
        /// </summary>
        /// <param name="operation">The operation.</param>
        /// <param name="context">The context.</param>
        public void Apply(Operation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
            {
                operation.Parameters = new List<IParameter>();
            }

            operation.Parameters.Add(new NonBodyParameter
            {
                Name = Constants.TenantId,
                In = Constants.Header,
                Description = Constants.TenantId,
                Required = true,
                Type = Constants.StringInText
            });
        }
    }
}
