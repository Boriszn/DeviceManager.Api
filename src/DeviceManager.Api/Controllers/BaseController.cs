using DeviceManager.Api.Constants;
using DeviceManager.Api.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace DeviceManager.Api.Controllers
{
    /// <summary>
    /// Base api controller all api controllers should inherit from this controller
    /// </summary>
    /// <typeparam name="TViewModel">Type of the view model</typeparam>
#if UseAuthentication
    [Authorize]
#endif
    public abstract class BaseController<TViewModel> : Controller where TViewModel : class
    {
        /// <summary>
        /// Shared resource file
        /// </summary>
        protected readonly IStringLocalizer<SharedResource> SharedLocalizer;

        /// <summary>
        /// TODO: Pass base validation service and common CRUD operation in the base controller
        /// </summary>
        public BaseController(IStringLocalizer<SharedResource> sharedLocalizer)
        {
            this.SharedLocalizer = sharedLocalizer;
        }

        /// <summary>
        /// Ping method to test the status of the service
        /// </summary>
        /// <returns>Based on the ui culture hello message is picked from the shared resource file and sent back</returns>
        [HttpGet(nameof(Ping))]
        [SwaggerOperation(nameof(Ping))]
        public IActionResult Ping() => Json(SharedLocalizer[DefaultConstants.Hello].Value);
    }
}