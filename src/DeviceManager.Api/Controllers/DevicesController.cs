using System;
using System.ComponentModel.DataAnnotations;
using DeviceManager.Api.ActionFilters;
using DeviceManager.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace DeviceManager.Api.Controllers
{
    [Route("api/devices")]
    public class DevicesController : Controller
    {
        private readonly IDeviceService deviceService;

        /// <summary>
        /// Initializes a new instance of the <see cref="DevicesController"/> class.
        /// </summary>
        /// <param name="deviceService">The device service.</param>
        public DevicesController(IDeviceService deviceService)
        {
            this.deviceService = deviceService;
        }

        /// <summary>
        /// Gets the specified device identifier.
        /// </summary>
        /// <param name="deviceId">The device identifier.</param>
        /// <param name="deviceTitle">Title of the device.</param>
        /// <returns></returns>
        [HttpGet("{deviceId}")]
        [SwaggerOperation("GetDeviceById")]
        [ValidateActionParameters]
        public IActionResult Get(string deviceId, [Required]string deviceTitle)
        {
            if (!this.ModelState.IsValid)
            {
                return new BadRequestObjectResult(this.ModelState);
            }

            return new ObjectResult(deviceService.GetDeviceById(Guid.Parse(deviceId)));
        }
    }
}
