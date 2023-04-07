using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Shared.Model;

namespace BuildingBlocks.Shared.Middleware
{
    public class GlobalExceptionHandler
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandler> _logger;
        public GlobalExceptionHandler(
            RequestDelegate next,
            ILogger<GlobalExceptionHandler> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong: {ex}");
                await HandleExceptionAsync(httpContext, ex);
            }
        }
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var statusCode = GetStatusCode(exception, context);
            context.Response.ContentType = "application/json";
            var response = new ErrorResponse
            {
                Title = "Request could not be processed!",
                Status = statusCode,
                Message = statusCode != StatusCodes.Status422UnprocessableEntity ? exception.Message : String.Empty,
                Errors = GetErrors(exception)
            };

            context.Response.StatusCode = statusCode;
            await context.Response?.WriteAsync(JsonConvert.SerializeObject(response));
        }

        private static int GetStatusCode(Exception exception, HttpContext context)
        {
            if (exception is ValidationException)//Check for fluent validation
                return StatusCodes.Status422UnprocessableEntity;
            else return StatusCodes.Status500InternalServerError;
        }
        private static IReadOnlyDictionary<string, string> GetErrors(Exception exception)
        {
            IReadOnlyDictionary<string, string> errors = null;
            if (exception is ValidationException validationException)
            {
                errors = validationException.Errors.ToDictionary(x => x.PropertyName, y => y.ErrorMessage);
            }
            return errors;
        }
    }
}
