namespace Customer.Domain.DomainEvents;

using Customer.Domain.Entities;
using MediatR;

public sealed class AccountAtMinimumBalanceDomainEvent : INotification
{
    public AccountAtMinimumBalanceDomainEvent(Account account)
    {
        Account = account;
    }
    public Account Account { get; set; }
}
