using System.Collections.Generic;
using DeviceManager.Api.Constants;
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
                Name = DefaultConstants.TenantId,
                Default = "b0ed668d-7ef2-4a23-a333-94ad278f45d7",
                In = DefaultConstants.Header,
                Description = DefaultConstants.TenantIdSwaggerDescription,
                Required = true,
                Type = DefaultConstants.StringInText
            });
        }
    }
}
