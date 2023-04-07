using BuildingBlocks.Repository.Service;
using Customer.Application.Dtos;
using Customer.Domain.Entities;
using Customer.Domain.Exceptions;
using Shared.Constants;

namespace Customer.Application.Queries
{
    internal sealed class GetCustomerBalanceQueryHandler : IRequestHandler<GetCustomerBalanceQuery, GetCustomerBalanceDto>
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
        public async Task<GetCustomerBalanceDto> Handle(GetCustomerBalanceQuery request, CancellationToken cancellationToken)
        {
            Account account = await _unitOfWork.GetRepositoryAsync<Account>().FirstOrDefaultAsync(f => f.Id == request.AccountId);
            if (account == null || account.Id == 0)
                throw new CustomerDomainException(CustomerServiceConstants.Messages.InvalidAccount);

            GetCustomerBalanceDto response = new GetCustomerBalanceDto()
            {
                AccountId = account.Id,
                Balance = account.Balance,
            };
            return response;
        }
        #endregion
    }
}
