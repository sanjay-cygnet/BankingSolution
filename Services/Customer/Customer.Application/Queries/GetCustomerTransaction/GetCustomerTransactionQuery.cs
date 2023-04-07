using Customer.Application.Dtos;

namespace Customer.Application.Queries
{
    public sealed class GetCustomerTransactionQuery : IRequest<List<GetCustomerTransactionDto>>
    {
        public GetCustomerTransactionQuery()
        {
        }
        public int AccountId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}