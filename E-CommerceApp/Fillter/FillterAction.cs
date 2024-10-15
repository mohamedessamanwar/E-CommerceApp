using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace E_CommerceApp.Fillter
{
    public class FillterAction : IActionFilter
    {
        private readonly ILogger<FillterAction> _logger;

        public FillterAction(ILogger<FillterAction> logger)
        {
            _logger = logger;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            Debug.WriteLine("mmmmmmmmm");
            _logger.LogInformation("mmmmmmmmm");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        
            _logger.LogInformation("After action execution");
        }
    }
}
