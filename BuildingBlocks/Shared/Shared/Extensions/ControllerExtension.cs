namespace BuildingBlocks.Shared.Extensions;

using Microsoft.AspNetCore.Mvc;
using Shared.Model;

public static class ControllerExtension
{
    public static IActionResult ApiResponse<T>(this ControllerBase controller, ApiResponse<T> response)
    {
        if (response.Success)//success result
        {
            return controller.Ok(response);
        }
        else
        {
            return controller.StatusCode(response.StatusCode, response);
        }
    }
}
