using System;
using System.ComponentModel.DataAnnotations;
using DeviceManager.Api.ActionFilters;
using DeviceManager.Api.Model;
using DeviceManager.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace DeviceManager.Api.Controllers
{
    [Route("api/v{version:apiVersion}/devices")]
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
        /// Gets the specified page.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        [HttpGet]
        [SwaggerOperation("GetDevices")]
        [ValidateActionParameters]
        public IActionResult Get([FromQuery, Required]int page, [FromQuery, Required]int pageSize)
        {
            return new ObjectResult(deviceService.GetDevices(page, pageSize));
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
        public IActionResult GetDeviceById([FromRoute][Required]string deviceId)
        {
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
            return new ObjectResult(deviceService.GetDeviceByTitle(deviceTitle));
        }

        /// <summary>
        /// Posts the specified device view model.
        /// </summary>
        /// <param name="deviceViewModel">The device view model.</param>
        /// <returns></returns>
        [HttpPost]
        [SwaggerOperation("CreateDevice")]
        [SwaggerResponse(204, null, "Device was saved successfuly")]
        [SwaggerResponse(400, null, "Error in saving the Device")]
        public IActionResult Post([FromBody]DeviceViewModel deviceViewModel)
        {
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
            deviceService.UpdateDevice(deviceId, deviceViewModel);

            return new OkResult();
        }
    }
}
