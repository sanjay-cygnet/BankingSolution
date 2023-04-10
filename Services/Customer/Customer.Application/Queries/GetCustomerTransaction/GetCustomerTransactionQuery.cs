namespace Customer.Application.Queries;

using BuildingBlocks.Shared.Model;
using Customer.Application.Dtos;

public sealed class GetCustomerTransactionQuery : IRequest<ApiResponse<List<GetCustomerTransactionDto>>>
{
    public int AccountId { get; set; }
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
}