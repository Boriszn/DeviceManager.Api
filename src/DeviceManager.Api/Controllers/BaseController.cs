using Microsoft.AspNetCore.Mvc;

namespace DeviceManager.Api.Controllers
{
    /// <summary>
    /// Base api controller all api controllers should inherit from this controller
    /// </summary>
    /// <typeparam name="TViewModel">Type of the view model</typeparam>
    public class BaseController<TViewModel> : Controller where TViewModel : class
    {
        /// <summary>
        /// TODO: Pass base validation service and common CRUD operation in the base controller
        /// </summary>
        public BaseController()
        {
        }
    }
}