using Microsoft.AspNetCore.Mvc.Controllers;
using Serilog;

namespace EagleRockService.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                LogActionEntry(httpContext);
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                LogError(ex, httpContext);
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await httpContext.Response.WriteAsync("Something went wrong. Please try again later.");
            }
        }

        private static void LogActionEntry(HttpContext httpContext)
        {
            var controllerActionDescriptor = httpContext?
                .GetEndpoint()
                ?.Metadata
                .GetMetadata<ControllerActionDescriptor>();

            var controllerName = controllerActionDescriptor?.ControllerName;
            var controllerActionName = controllerActionDescriptor?.ActionName;

            Log.Information("Entering {ControllerName} {ControllerAction} action.", controllerName,
                controllerActionName);
        }

        private void LogError(Exception ex, HttpContext httpContext)
        {
            var controllerActionDescriptor = httpContext?
                .GetEndpoint()
                ?.Metadata
                .GetMetadata<ControllerActionDescriptor>();

            var controllerName = controllerActionDescriptor?.ControllerName;
            var controllerActionName = controllerActionDescriptor?.ActionName;
            var httpPath = httpContext?.Request.Path;
            var httpMethod = httpContext?.Request.Method;

            Log.Error(ex,
                "An unhandled exception occurred in action {ControllerName} {ControllerAction} - [{HttpMethod}]{HttpPath}. Message: {ExceptionMessage}",
                controllerName, controllerActionName, httpMethod, httpPath, ex.Message);
        }
    }
}
