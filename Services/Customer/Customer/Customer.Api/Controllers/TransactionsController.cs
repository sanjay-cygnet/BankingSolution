﻿namespace Customer.Api.Controllers;

using BuildingBlocks.Shared.Constants;
using BuildingBlocks.Shared.Extensions;
using BuildingBlocks.Shared.Model;
using Customer.Application.Dtos;
using Customer.Application.Queries;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

/// <summary>
/// 
/// </summary>
/// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
[ApiController]
[Route(GlobalConstants.Route)]
public class TransactionsController : Controller
{
    #region Members
    private readonly ISender _sender;
    #endregion

    #region Ctor
    public TransactionsController(ISender sender)
    {
        _sender = sender;
    }
    #endregion

    #region Method(s)

    /// <summary>
    /// Gets the customer transaction.
    /// </summary>
    /// <returns></returns>
    [HttpPost("get-transactions")]
    [SwaggerResponse(StatusCodes.Status200OK, null, typeof(ApiResponse<List<GetCustomerTransactionDto>>))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, null, typeof(ApiResponse<List<GetCustomerTransactionDto>>))]
    public async Task<IActionResult> GetCustomerTransactions([FromBody] GetCustomerTransactionQuery request)
    {
        var result = await _sender.Send(request);
        return this.ApiResponse(result);
    }
    #endregion
}
