using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using DeviceManager.Api.ActionFilters;
using DeviceManager.Api.Model;
using DeviceManager.Api.Resources;
using DeviceManager.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace DeviceManager.Api.Controllers
{
    /// <summary>
    /// CRUD operations of the Device 
    /// </summary>
    [Route("api/v{version:apiVersion}/devices")]
    public class DevicesController : BaseController<DeviceViewModel>
    {
        private readonly IDeviceService deviceService;

        /// <summary>
        /// Initializes a new instance of the <see cref="DevicesController"/> class.
        /// </summary>
        /// <param name="deviceService">The device service.</param>
        /// <param name="sharedLocalizer">Global resource file</param>
        public DevicesController(IDeviceService deviceService, IStringLocalizer<SharedResource> sharedLocalizer) 
            : base(sharedLocalizer)
        {
            this.deviceService = deviceService;
        }

        #region Entity Framework Core

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
        /// <returns></returns>
        [HttpGet("{deviceId}")]
        [SwaggerOperation("GetDeviceById")]
        [ValidateActionParameters]
        public IActionResult GetDeviceById([FromRoute][Required]string deviceId)
        {
            return new ObjectResult(deviceService.GetDeviceById(Guid.Parse(deviceId)));
        }

        /// <summary>
        /// Gets the specified device identifier in async await pattern.
        /// </summary>
        /// <param name="deviceId">The device identifier.</param>
        /// <returns></returns>
        [HttpGet("async/{deviceId}")]
        [SwaggerOperation(nameof(GetDeviceByIdAsync))]
        [ValidateActionParameters]
        public async Task<IActionResult> GetDeviceByIdAsync([FromRoute][Required]string deviceId)
        {
            return new ObjectResult(await deviceService.GetDeviceByIdAsync(Guid.Parse(deviceId)));
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

        #endregion

        #region Dapper

        /// <summary>
        /// Gets all records using dapper.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        [HttpGet("dapper")]
        [SwaggerOperation("GetAllDevicesUsingDapper")]
        [ValidateActionParameters]
        public async Task<IActionResult> GetAllUsingDapper([FromQuery, Required]int page, [FromQuery, Required]int pageSize)
        {
            return new ObjectResult(await deviceService.GetDevicesUsingDapper(page, pageSize));
        }

        /// <summary>
        /// Gets the specified device identifier in async await pattern using dapper.
        /// </summary>
        /// <param name="deviceId">The device identifier.</param>
        /// <returns></returns>
        [HttpGet("dapper/async/{deviceId}")]
        [SwaggerOperation(nameof(GetDeviceByIdAsync))]
        [ValidateActionParameters]
        public async Task<IActionResult> GetDeviceByIdUsingDapperAsync([FromRoute][Required]string deviceId)
        {
            return new ObjectResult(await deviceService.GetDeviceByIdUsingDapperAsync(Guid.Parse(deviceId)));
        }

        /// <summary>
        /// Posts the specified device view model and executeds query using dapper.
        /// </summary>
        /// <param name="deviceViewModel">The device view model.</param>
        /// <returns></returns>
        [HttpPost("dapper")]
        [SwaggerOperation("CreateDeviceUsingDapper")]
        [SwaggerResponse(204, null, "Device was saved successfuly")]
        [SwaggerResponse(400, null, "Error in saving the Device")]
        public async Task<IActionResult> PostUsingDapepr([FromBody]DeviceViewModel deviceViewModel)
        {
            await deviceService.CreateDeviceUsingDapperAsync(deviceViewModel);

            return new OkResult();
        }

        #endregion
    }
}
