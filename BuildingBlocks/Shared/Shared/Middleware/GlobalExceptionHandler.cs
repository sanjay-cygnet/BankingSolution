namespace BuildingBlocks.Shared.Middleware;

using BuildingBlocks.Shared.Constants;
using BuildingBlocks.Shared.Model;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

public class GlobalExceptionHandler
{
    #region Members
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionHandler> _logger;
    #endregion

    #region Ctor
    public GlobalExceptionHandler(
       RequestDelegate next,
       ILogger<GlobalExceptionHandler> logger)
    {
        _next = next;
        _logger = logger;
    }
    #endregion

    #region Method(s)
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
        var response = new ApiResponse<GlobalExceptionHandler>()
        {
            Success = false,
            StatusCode = statusCode,
            Error = new ErrorResponse
            {
                Title = Validations.RequestCouldNotProcessed,
                Message = statusCode != StatusCodes.Status422UnprocessableEntity ? exception.Message : String.Empty,
                Errors = GetErrors(exception)
            }
        };

        context.Response.StatusCode = statusCode;
        await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
    }

    private static int GetStatusCode(Exception exception, HttpContext context)
    {
        if (exception is ValidationException)//Check for fluent validation
            return StatusCodes.Status422UnprocessableEntity;
        else return StatusCodes.Status500InternalServerError;
    }

    private static List<ErrorDetail>? GetErrors(Exception exception)
    {
        List<ErrorDetail>? errors = null;
        if (exception is ValidationException validationException)
        {
            errors = validationException.Errors.Select(s => new ErrorDetail
            {
                Field = s.PropertyName,
                Validation = s.ErrorMessage
            }).ToList();
        }
        return errors;
    }
    #endregion
}
