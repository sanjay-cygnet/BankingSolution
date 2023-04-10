namespace Customer.Application.Queries;

using AutoMapper;
using BuildingBlocks.Repository.Service;
using BuildingBlocks.Shared.Model;
using Customer.Application.Dtos;
using Customer.Domain.Entities;
using System.Linq;

internal sealed class GetCustomerTransactionQueryHandler : IRequestHandler<GetCustomerTransactionQuery, ApiResponse<List<GetCustomerTransactionDto>>>
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
    public Task<ApiResponse<List<GetCustomerTransactionDto>>> Handle(GetCustomerTransactionQuery request, CancellationToken cancellationToken)
    {
        var transactions = _unitOfWork.GetRepositoryAsync<Transaction>().Query(q => q.AccountId == request.AccountId && q.Status == (short)TransactionStatusEnum.Success && request.FromDate <= q.EffectiveDate && q.EffectiveDate <= request.ToDate).ToList();

        if (!transactions.Any())
        {
            return Task.FromResult(new ApiResponse<List<GetCustomerTransactionDto>>(HttpStatusCode.NotFound.ToInt(), CustomerServiceConstants.Messages.NoTransactionsFound));
        }

        var transactionsDto = transactions.Select(s => new GetCustomerTransactionDto()
        {
            ActualTransactionDate = s.ActualTransactionDate,
            Amount = s.Amount,
            TransactionNumber = s.TransactionNumber,
            TransactionType = s.TransactionType > 0 ? Enum.Parse<TransactionTypeEnum>(s.TransactionType.ToString()).ToString() : String.Empty
        }).ToList();

        return Task.FromResult(new ApiResponse<List<GetCustomerTransactionDto>>(transactionsDto));
    }
    #endregion
}