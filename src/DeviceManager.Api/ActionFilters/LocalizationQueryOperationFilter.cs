using DeviceManager.Api.Helpers;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;

namespace DeviceManager.Api.ActionFilters
{
    /// <summary>
    /// Adds culture and ui-culture into query parameters
    /// </summary>
    public class LocalizationQueryOperationFilter : IOperationFilter
    {
        /// <inherit/>
        public void Apply(Operation operation, OperationFilterContext context)
        {
            operation.Parameters.Add(new NonBodyParameter
            {
                Name = Constants.UiCulture,
                In = Constants.Query,
                Description = Constants.UiCulture,
                Required = false,
                Type = Constants.StringInText
            });

            operation.Parameters.Add(new NonBodyParameter
            {
                Name = Constants.Culture,
                In = Constants.Query,
                Description = Constants.Culture,
                Default = Constants.EnglishCulture,
                Required = false,
                Type = Constants.StringInText
            });
        }
    }
}
