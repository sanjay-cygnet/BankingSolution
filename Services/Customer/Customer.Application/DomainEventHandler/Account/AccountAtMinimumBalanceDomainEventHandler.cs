namespace Customer.Application.DomainEventHandler;

using BuildingBlocks.EventBus.EventPublisherModel;
using BuildingBlocks.EventBus.QueuePublisher;
using Customer.Domain.DomainEvents;

internal sealed class AccountAtMinimumBalanceDomainEventHandler : INotificationHandler<AccountAtMinimumBalanceDomainEvent>
{
    #region Members
    private readonly IEmailQueuePublisher _emailQueuePublisher;
    #endregion

    #region Ctor
    public AccountAtMinimumBalanceDomainEventHandler(
        IEmailQueuePublisher emailQueuePublisher)
    {
        _emailQueuePublisher = emailQueuePublisher;
    }
    #endregion

    #region Method(s)
    public async Task Handle(AccountAtMinimumBalanceDomainEvent notification, CancellationToken cancellationToken)
    {
        await _emailQueuePublisher.Publish(new EmailPublisherModel() { TemplateId = 1, Subject = "Sending Mail to customer to maintain Minimum balance" });
    }
    #endregion
}
