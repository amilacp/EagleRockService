using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace EagleRockService.Controllers
{
    public class MediatorControllerBase : ControllerBase
    {
        protected readonly IMediator Mediator;

        public MediatorControllerBase(IMediator mediator)
        {
            Mediator = mediator;
        }
        protected string ActionName => ControllerContext.ActionDescriptor.ActionName;
        protected string ControllerName => ControllerContext.ActionDescriptor.ControllerName;

        private void LogOkResult()
        {
            Log.Information("Exiting {ControllerName} {ControllerAction} action with Ok result.", ControllerName,
                ActionName);
        }

        private void LogNoContentResult()
        {
            Log.Information("Exiting {ControllerName} {ControllerAction} action with No Content result.", ControllerName,
                ActionName);
        }

        private void LogBadRequestResult()
        {
            Log.Information("Exiting {ControllerName} {ControllerAction} action with Bad Request result.", ControllerName,
                ActionName);
        }

        private void LogForbiddenResult()
        {
            Log.Information("Exiting {ControllerName} {ControllerAction} action with Forbidden result.", ControllerName,
                ActionName);
        }

        private void LogUnauthorizedResult()
        {
            Log.Information("Exiting {ControllerName} {ControllerAction} action with Unauthorized result.", ControllerName,
                ActionName);
        }

        private void LogConflictResult()
        {
            Log.Information("Exiting {ControllerName} {ControllerAction} action with Conflict result.", ControllerName,
                ActionName);
        }

        private void LogUnprocessableResult()
        {
            Log.Information("Exiting {ControllerName} {ControllerAction} action with Unprocessable result.", ControllerName,
                ActionName);
        }

        private void LogRedirectResult()
        {
            Log.Information("Exiting {ControllerName} {ControllerAction} action with Redirect result.", ControllerName,
                ActionName);
        }

        private void LogCreatedResult()
        {
            Log.Information("Exiting {ControllerName} {ControllerAction} action with Created result.", ControllerName,
                ActionName);
        }

        private void LogErrorResult()
        {
            Log.Information("Exiting {ControllerName} {ControllerAction} action with Internal Server Error.", ControllerName,
                ActionName);
        }

        protected IActionResult NoContentAndLog()
        {
            LogNoContentResult();
            return NoContent();
        }

        protected IActionResult BadRequestAndLog()
        {
            LogBadRequestResult();
            return BadRequest();
        }

        protected IActionResult ForbiddenAndLog()
        {
            LogForbiddenResult();
            return Forbid();
        }

        protected IActionResult UnauthorizedAndLog()
        {
            LogUnauthorizedResult();
            return Unauthorized();
        }

        protected IActionResult ConflictAndLog()
        {
            LogConflictResult();
            return Conflict();
        }

        protected IActionResult UnprocessableAndLog()
        {
            LogUnprocessableResult();
            return UnprocessableEntity();
        }

        protected IActionResult ErrorAndLog()
        {
            LogErrorResult();
            return Problem();
        }

        protected IActionResult OkAndLog<T>(T response)
        {
            try
            {
                string resultSet;
                if (response is null)
                {
                    resultSet = "Unable to log the response";
                }

                Log.Information("{ControllerName} {ControllerAction} action with list response.",
                    ControllerName, ActionName);
            }
            catch (Exception ex)
            {
                Log.Error("Logging error", ex);
            }

            LogOkResult();
            return Ok(response);
        }
    }
}
