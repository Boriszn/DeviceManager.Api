using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using DeviceManager.Api.Constants;

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
                Name = DefaultConstants.UiCulture,
                In = DefaultConstants.Query,
                Description = DefaultConstants.UiCulture,
                Required = false,
                Type = DefaultConstants.StringInText
            });

            operation.Parameters.Add(new NonBodyParameter
            {
                Name = DefaultConstants.Culture,
                In = DefaultConstants.Query,
                Description = DefaultConstants.Culture,
                Default = DefaultConstants.EnglishCulture,
                Required = false,
                Type = DefaultConstants.StringInText
            });
        }
    }
}
