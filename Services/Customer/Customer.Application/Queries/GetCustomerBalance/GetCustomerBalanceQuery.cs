using Customer.Application.Dtos;

namespace Customer.Application.Queries
{
    public sealed class GetCustomerBalanceQuery : IRequest<GetCustomerBalanceDto>
    {
        public GetCustomerBalanceQuery()
        {
        }
        public GetCustomerBalanceQuery(int accountId)
        {
            AccountId = accountId;
        }
        public int AccountId { get; set; }
    }
}