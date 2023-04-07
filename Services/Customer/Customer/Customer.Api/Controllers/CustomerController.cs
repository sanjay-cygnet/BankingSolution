using BuildingBlocks.Shared.Constants;
using Customer.Application.Commands.TransferFunds;
using Customer.Application.Dtos;
using Customer.Application.Queries;
using Microsoft.AspNetCore.Mvc;
using Shared.Model;
using Swashbuckle.AspNetCore.Annotations;

namespace Customer.Api.Controllers
{
    /// <summary>
    /// Customer Controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [ApiController]
    [Route(GlobalConstants.Route)]
    public class CustomerController : ControllerBase
    {
        #region Members
        private readonly ISender _sender;
        #endregion

        #region Ctor
        public CustomerController(ISender sender)
        {
            _sender = sender;
        }
        #endregion

        #region Method(s)
        /// <summary>
        /// Gets the customer's balance.
        /// </summary>
        /// <param name="accountId">The account identifier.</param>
        /// <returns></returns>
        [HttpGet("balance/{accountId}")]
        [SwaggerResponse(StatusCodes.Status200OK, null, typeof(GetCustomerBalanceDto))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, null, typeof(ErrorResponse))]
        public async Task<IActionResult> GetCustomerBalance(int accountId)
        {
            var result = await _sender.Send(new GetCustomerBalanceQuery(accountId));
            return Ok(result);
        }
        #endregion
    }
}