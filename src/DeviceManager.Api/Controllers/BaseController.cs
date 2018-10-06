using DeviceManager.Api.Helpers;
using DeviceManager.Api.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace DeviceManager.Api.Controllers
{
    /// <summary>
    /// Base api controller all api controllers should inherit from this controller
    /// </summary>
    /// <typeparam name="TViewModel">Type of the view model</typeparam>
    public abstract class BaseController<TViewModel> : Controller where TViewModel : class
    {
        /// <summary>
        /// Shared resource file
        /// </summary>
        protected readonly IStringLocalizer<SharedResource> sharedLocalizer;

        /// <summary>
        /// TODO: Pass base validation service and common CRUD operation in the base controller
        /// </summary>
        public BaseController(IStringLocalizer<SharedResource> sharedLocalizer)
        {
            this.sharedLocalizer = sharedLocalizer;
        }

        /// <summary>
        /// Ping method to test the status of the service
        /// </summary>
        /// <returns>Based on the ui culture hello message is picked from the shared resource file and sent back</returns>
        [HttpGet(nameof(Hello))]
        [SwaggerOperation(nameof(Hello))]
        public IActionResult Hello() => Json(sharedLocalizer[Constants.Hello].Value);
    }
}