using BuildingBlocks.Shared.Constants;
using BuildingBlocks.Shared.Extensions;
using Shared.Extensions;
using System.Net;
using System.Text.Json.Serialization;
using JsonIgnoreAttribute = System.Text.Json.Serialization.JsonIgnoreAttribute;

namespace Shared.Model
{
    public class ApiResponse<T>
    {
        public ApiResponse()
        {
            StatusCode = HttpStatusCode.OK.ToInt();
        }

        public ApiResponse(T results)//Used for success response
        {
            StatusCode = HttpStatusCode.OK.ToInt();
            Success = true;
            Result = results;
        }

        public ApiResponse(
            int statusCode,
            string errorMessage,
            string? title = null,
            ErrorResponse? errors = null)//Used  for fail response
        {
            Success = false;
            StatusCode = statusCode;
            Error = errors is null && !errorMessage.IsNull() ? new ErrorResponse() { Message = errorMessage, Title = title.IsNull() ? Validations.RequestCouldNotProcessed : title } : errors;
        }

        public bool Success { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public int StatusCode { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public T? Result { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ErrorResponse? Error { get; set; }
    }
}

public class ErrorResponse
{
    public ErrorResponse()
    { }

    public ErrorResponse(string message)
    {
        Message = message;
    }

    public ErrorResponse(string title, string message)
    {
        Title = title;
        Message = message;
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<ErrorDetail>? Errors { get; set; }
}

public class ErrorDetail
{
    public string Field { get; set; } = String.Empty;
    public string Validation { get; set; } = String.Empty;
}