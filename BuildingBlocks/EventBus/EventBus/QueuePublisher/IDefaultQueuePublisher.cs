namespace BuildingBlocks.EventBus.QueuePublisher
{
    public interface IDefaultQueuePublisher
    {
        Task Publish(string queueName, string queuData);
    }
}
