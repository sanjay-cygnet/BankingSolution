namespace Customer.Application.Queries;

using BuildingBlocks.Repository.Service;
using BuildingBlocks.Shared.Model;
using Customer.Application.Dtos;
using Customer.Domain.Entities;

internal sealed class GetCustomerBalanceQueryHandler : IRequestHandler<GetCustomerBalanceQuery, ApiResponse<GetCustomerBalanceDto>>
{
    #region Members
    private readonly IUnitOfWork _unitOfWork;
    #endregion

    #region ctor
    public GetCustomerBalanceQueryHandler(
   IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    #endregion

    #region Method(s)
    public async Task<ApiResponse<GetCustomerBalanceDto>> Handle(GetCustomerBalanceQuery request, CancellationToken cancellationToken)
    {
        Account? account = await _unitOfWork.GetRepositoryAsync<Account>().FirstOrDefaultAsync(f => f.Id == request.AccountId);
        if (account is null)
            return new ApiResponse<GetCustomerBalanceDto>(HttpStatusCode.NotFound.ToInt(), CustomerServiceConstants.Messages.InvalidAccount);

        return new ApiResponse<GetCustomerBalanceDto>(new GetCustomerBalanceDto()
        {
            AccountId = account.Id,
            Balance = account.Balance,
        });
    }
    #endregion
}
