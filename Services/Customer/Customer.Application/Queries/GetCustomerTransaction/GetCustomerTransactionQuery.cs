using Customer.Application.Dtos;
namespace Customer.Application.Queries;

public sealed class GetCustomerTransactionQuery : IRequest<ApiResponse<List<GetCustomerTransactionDto>>>
{
    public int AccountId { get; set; }
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
}