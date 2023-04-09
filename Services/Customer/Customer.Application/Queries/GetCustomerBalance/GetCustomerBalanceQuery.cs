namespace Customer.Application.Queries;

using Customer.Application.Dtos;

public sealed class GetCustomerBalanceQuery : IRequest<ApiResponse<GetCustomerBalanceDto>>
{
    public GetCustomerBalanceQuery() { }
    public GetCustomerBalanceQuery(int accountId)
    {
        AccountId = accountId;
    }
    public int AccountId { get; set; }
}
