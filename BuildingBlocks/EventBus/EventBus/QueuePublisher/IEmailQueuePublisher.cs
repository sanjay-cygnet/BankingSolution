using BuildingBlocks.EventBus.EventPublisherModel;

namespace BuildingBlocks.EventBus.QueuePublisher
{
    public interface IEmailQueuePublisher
    {
        Task Publish(EmailPublisherModel model);
    }
}
