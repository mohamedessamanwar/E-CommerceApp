using Microsoft.AspNetCore.Mvc.Filters;

namespace E_CommerceApp.Fillter
{
    public class LogActivity : IAsyncActionFilter
    {
        private readonly ILogger<LogActivity> _logger;

        public LogActivity(ILogger<LogActivity> logger)
        {
            _logger = logger;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // Get HttpContext
            var httpContext = context.HttpContext;

            // Get IP Address
            var ipAddress = httpContext.Connection.RemoteIpAddress?.ToString();

            // Get Browser Details
            var userAgent = httpContext.Request.Headers["User-Agent"].FirstOrDefault();

            // Log Request Details
            _logger.LogInformation("Start Action {ActionName} executing at {Time}", context.ActionDescriptor.DisplayName, DateTime.UtcNow);
            _logger.LogInformation("Client IP: {IpAddress}, User Agent: {UserAgent}", ipAddress, userAgent);
   
            // Call the next delegate/middleware in the pipeline
            var resultContext = await next();

            // Log after the action executes
            _logger.LogInformation("End Action {ActionName} executed at {Time}", resultContext.ActionDescriptor.DisplayName, DateTime.UtcNow);
        }
    }
    }
