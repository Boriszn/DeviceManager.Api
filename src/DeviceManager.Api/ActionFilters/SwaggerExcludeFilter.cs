using System;
using System.Linq;
using DeviceManager.Api.Helpers;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace DeviceManager.Api.ActionFilters
{
    /// <summary>
    /// Ignores all the properties for the request
    /// </summary>
    public class SwaggerExcludeFilter : ISchemaFilter
    {
        /// <inherit/>
        public void Apply(Schema model, SchemaFilterContext context)
        {
            var excludeProperties = context.SystemType?.GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(SwaggerExcludeAttribute)));
            foreach (var property in excludeProperties)
            {
                // Because swagger uses camel casing
                var propertyName = $"{Char.ToLowerInvariant(property.Name[0])}{property.Name.Substring(1)}";
                if (model.Properties.ContainsKey(propertyName))
                {
                    model.Properties.Remove(propertyName);
                }
            }
        }
    }
}
