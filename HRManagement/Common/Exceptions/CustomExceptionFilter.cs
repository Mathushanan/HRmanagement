using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Microsoft.AspNetCore.Http;

namespace HRManagement.Common.Exceptions
{
    public class CustomExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<CustomExceptionFilter> _logger;

        public CustomExceptionFilter(ILogger<CustomExceptionFilter> logger)
        {
            _logger = logger;
        }

        // Log the exception details & Mark the exception as handled
        public void OnException(ExceptionContext context)
        {
            _logger.LogError(context.Exception, "An unhandled exception occurred.");

            var problemDetails = new ProblemDetails
            {
                Status = (int)HttpStatusCode.InternalServerError,
                Title = "An error occurred while processing your request.",
                Detail = context.Exception.Message,
                Instance = context.HttpContext.Request.Path
            };

            
            context.Result = new ObjectResult(problemDetails)
            {
                StatusCode = (int)HttpStatusCode.InternalServerError
            };

          
            context.ExceptionHandled = true;
        }

        
    }

}
