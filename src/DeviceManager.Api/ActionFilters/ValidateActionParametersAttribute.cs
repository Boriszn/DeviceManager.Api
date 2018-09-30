using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DeviceManager.Api.ActionFilters
{
    /// <inheritdoc />
    public class ValidateActionParametersAttribute : ActionFilterAttribute
    {
        private const string RequiredAttributeKey = "RequiredAttribute";

        /// <inheritdoc />
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var descriptor = context.ActionDescriptor as ControllerActionDescriptor;

            if (descriptor != null)
            {
                var parameters = descriptor.MethodInfo.GetParameters();

                CheckParameterRequired(context, parameters);
            }

            base.OnActionExecuting(context);
        }

        private static void CheckParameterRequired(ActionExecutingContext context, IEnumerable<ParameterInfo> parameters)
        {
            foreach (var parameter in parameters)
            {
                if (parameter.CustomAttributes.Any() && parameter.CustomAttributes.Select(item => item.AttributeType
                        .ToString()
                        .Contains(RequiredAttributeKey)).Any())
                {
                    if (!context.ActionArguments.Keys.Contains(parameter.Name))
                    {
                        context.ModelState.AddModelError(parameter.Name, $"Parameter {parameter.Name} is required");
                    }
                }
            }

            if (context.ModelState.ErrorCount != 0)
            {
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
        }
    }
}
