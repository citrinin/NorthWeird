using System;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace NorthWeird.WebUI.Filters
{
    public class LoggingActionFilterAttribute : IActionFilter
    {
        private readonly ILogger _logger;
        private readonly bool _enableLoggingParameters;

        public LoggingActionFilterAttribute(ILogger<LoggingActionFilterAttribute> logger, bool enableLoggingParameters = false)
        {
            _logger = logger;
            _enableLoggingParameters = enableLoggingParameters;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.LogInformation($"Before method {context.ActionDescriptor.DisplayName} called");
            if (_enableLoggingParameters)
            {
                _logger.LogInformation("Parameters");
                foreach (var contextActionArgument in context.ActionArguments)
                {
                    _logger.LogInformation($"parameter: {contextActionArgument.Key} - value: {JsonConvert.SerializeObject(contextActionArgument.Value)}");
                }
            }

        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.LogInformation($"After method {context.ActionDescriptor.DisplayName} called");
        }
    }
}
