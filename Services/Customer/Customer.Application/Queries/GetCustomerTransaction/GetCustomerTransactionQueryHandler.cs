using AutoMapper;
using BuildingBlocks.Repository.Service;
using Customer.Application.Dtos;
using Customer.Domain.Entities;
using Customer.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;
using Shared.Constants;
using Shared.Enum;

namespace Customer.Application.Queries
{
    internal sealed class GetCustomerTransactionQueryHandler : IRequestHandler<GetCustomerTransactionQuery, List<GetCustomerTransactionDto>>
    {
        #region Members
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        #endregion

        #region ctor
        public GetCustomerTransactionQueryHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        #endregion

        #region Method(s)
        public async Task<List<GetCustomerTransactionDto>> Handle(GetCustomerTransactionQuery request, CancellationToken cancellationToken)
        {
            var transactions = _unitOfWork.GetRepositoryAsync<Transaction>().Query(q => q.AccountId == request.AccountId && q.Status == (short)TransactionStatusEnum.Success && request.FromDate <= q.EffectiveDate && q.EffectiveDate <= request.ToDate).ToList();//Get 

            if (!transactions.Any())
                throw new CustomerDomainException(CustomerServiceConstants.Messages.NoTransactionsFound);

            List<GetCustomerTransactionDto> transactionsDto = new List<GetCustomerTransactionDto>();
            _mapper.Map(transactions, transactionsDto);

            return transactionsDto;
        }
        #endregion
    }
}
