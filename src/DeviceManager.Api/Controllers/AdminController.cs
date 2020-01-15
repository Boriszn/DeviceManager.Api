using DeviceManager.Api.Configuration;
using DeviceManager.Api.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;

namespace DeviceManager.Api.Controllers
{
    /// <summary>
    /// Sample controller to showcase authorization.
    /// All the resources in this controller are accessed only by users whose role belong to <see cref="PolicyConstants.Admin"/>.
    /// This is validated in <see cref="AuthenticationConfiguration.Configure"/> method
    /// </summary>
    [Route("api/v{version:apiVersion}/Admin")]
# if UseAuthentication
    [Authorize(PolicyConstants.Admin)]
#endif
    public class AdminController
    {
        /// <summary>
        /// Returns a list of string
        /// </summary>
        /// <returns>a list of string</returns>
        [HttpGet]
        [SwaggerOperation(nameof(GetData))]
        public IActionResult GetData()
        {
            return new ObjectResult(new List<string> { "1", "2" });
        }
    }
}