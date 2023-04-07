using BuildingBlocks.Shared.Constants;
using Customer.Application.Commands.TransferFunds;
using Microsoft.AspNetCore.Mvc;
using Shared.Model;
using Swashbuckle.AspNetCore.Annotations;

namespace Customer.Api.Controllers
{
    [ApiController]
    [Route(GlobalConstants.Route)]
    public class AccountController : Controller
    {
        #region Members
        private readonly ISender _sender;
        #endregion

        #region Ctor
        public AccountController(ISender sender)
        {
            _sender = sender;
        }
        #endregion

        #region Method(s)
        /// <summary>
        /// Gets the customer transaction.
        /// </summary>
        /// <returns></returns>
        [HttpPost("transfer-fund")]
        [SwaggerResponse(StatusCodes.Status200OK, null, typeof(bool))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, null, typeof(ErrorResponse))]
        public async Task<IActionResult> TransferFunds([FromBody] TransferFundsCommand request)
        {
            var result = await _sender.Send(request);
            return Ok(result);
        }
        #endregion
    }
}
