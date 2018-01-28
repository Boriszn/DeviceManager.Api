using System;
using System.ComponentModel.DataAnnotations;
using DeviceManager.Api.ActionFilters;
using DeviceManager.Api.Model;
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
        public IActionResult Get([FromRoute]string deviceId, [Required]string deviceTitle)
        {
            if (!this.ModelState.IsValid)
            {
                return new BadRequestObjectResult(this.ModelState);
            }

            return new ObjectResult(deviceService.GetDeviceById(Guid.Parse(deviceId)));
        }

        /// <summary>
        /// Gets the device by title.
        /// </summary>
        /// <param name="deviceTitle">The device title.</param>
        /// <returns></returns>
        [HttpGet("title/{deviceTitle}")]
        [SwaggerOperation("GetDeviceByTitle")]
        public IActionResult GetDeviceByTitle(string deviceTitle)
        {
            if (!this.ModelState.IsValid)
            {
                return new BadRequestObjectResult(this.ModelState);
            }

            return new ObjectResult(deviceService.GetDeviceByTitle(deviceTitle));
        }

        /// <summary>
        /// Posts the specified device view model.
        /// </summary>
        /// <param name="deviceViewModel">The device view model.</param>
        /// <returns></returns>
        [HttpPost]
        [SwaggerOperation("CreateDevice")]
        public IActionResult Post([FromBody]DeviceViewModel deviceViewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return new BadRequestObjectResult(this.ModelState);
            }

            deviceService.CreateDevice(deviceViewModel);

            return new OkResult();
        }

        /// <summary>
        /// Puts the specified device identifier.
        /// </summary>
        /// <param name="deviceId">The device identifier.</param>
        /// <param name="deviceViewModel">The device view model.</param>
        /// <returns></returns>
        [HttpPut("{deviceId}")]
        [SwaggerOperation("UpdateDevice")]
        public IActionResult Put([FromRoute]Guid deviceId, [FromBody]DeviceViewModel deviceViewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return new BadRequestObjectResult(this.ModelState);
            }

            deviceService.UpdateDevice(deviceId, deviceViewModel);

            return new OkResult();
        }
    }
}
