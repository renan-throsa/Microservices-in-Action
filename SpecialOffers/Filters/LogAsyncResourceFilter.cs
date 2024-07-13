using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace SpecialOffers.Filters
{
    public class LogAsyncResourceFilter : Attribute, IResourceFilter
    {
        private readonly ILogger<LogAsyncResourceFilter> _logger;
        private readonly Stopwatch _stopwatch;
        public LogAsyncResourceFilter(ILogger<LogAsyncResourceFilter> logger)
        {
            _logger = logger;
            _stopwatch = new Stopwatch();
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            _stopwatch.Start();
            var simplifiedRequest = new
            {
                context.HttpContext.Request.Path,
                context.HttpContext.Request.Method
            };
            _logger.LogInformation($"Processing request: {simplifiedRequest}");
        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            _stopwatch.Stop();
            _logger.LogInformation($"Processed request in {_stopwatch.Elapsed} with StatusCode: {context.HttpContext.Response.StatusCode}");
           
        }

        
        
    }
}
