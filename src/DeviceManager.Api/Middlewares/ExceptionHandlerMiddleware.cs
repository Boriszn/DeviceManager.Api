using System;
using System.Net;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace DeviceManager.Api.Middlewares
{
    /// <summary>
    /// Central error/exception handler Middleware
    /// </summary>
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate request;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionHandlerMiddleware"/> class.
        /// </summary>
        /// <param name="next">The next.</param>
        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            this.request = next;
        }

        /// <summary>
        /// Invokes the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public Task Invoke(HttpContext context) => this.InvokeAsync(context); 

        async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await this.request(context);
            }
            catch (Exception exception)
            {
                var httpStatusCode = ConfigurateExceptionTypes(exception);

                context.Response.StatusCode = httpStatusCode;
                await context.Response.WriteAsync(exception.Message);
                context.Response.Headers.Clear();
            }
        }

        private static int ConfigurateExceptionTypes(Exception exception)
        {
            int httpStatusCode = (int) HttpStatusCode.InternalServerError;

            if (exception is ValidationException)
            {
                httpStatusCode = (int) HttpStatusCode.BadRequest;
            }

            return httpStatusCode;
        }
    }
}
