using Customer.Domain.Entities;
using MediatR;

namespace Customer.Domain.DomainEvents
{
    public sealed class AccountBalanceReachedMinimumBalanceDomainEvent : INotification
    {
        public AccountBalanceReachedMinimumBalanceDomainEvent(Account account)
        {
            Account = account;
        }
        public Account Account { get; set; }
    }
}
