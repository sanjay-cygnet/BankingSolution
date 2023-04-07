using BuildingBlocks.EventBus.EventPublisherModel;
using BuildingBlocks.EventBus.QueuePublisher;
using Customer.Domain.DomainEvents;

namespace Customer.Application.DomainEventHandler
{
    internal sealed class AccountBalanceReachedMinimumBalanceDomainEventHandler : INotificationHandler<AccountBalanceReachedMinimumBalanceDomainEvent>
    {
        #region Members
        private readonly IEmailQueuePublisher _emailQueuePublisher;
        #endregion

        #region Ctor
        public AccountBalanceReachedMinimumBalanceDomainEventHandler(
            IEmailQueuePublisher emailQueuePublisher)
        {
            _emailQueuePublisher = emailQueuePublisher;
        }
        #endregion

        #region Method(s)
        public async Task Handle(AccountBalanceReachedMinimumBalanceDomainEvent notification, CancellationToken cancellationToken)
        {
            await _emailQueuePublisher.Publish(new EmailPublisherModel() { TemplateId = 1, Subject = "Sending Mail to customer to maintain Minimum balance" });
        }
        #endregion
    }
}
