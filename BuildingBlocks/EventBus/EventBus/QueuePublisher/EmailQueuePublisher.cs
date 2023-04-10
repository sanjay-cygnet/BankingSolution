namespace BuildingBlocks.EventBus.QueuePublisher;

using BuildingBlocks.EventBus.EventPublisherModel;
using Newtonsoft.Json;

internal class EmailQueuePublisher : IEmailQueuePublisher
{
    #region Members
    private readonly IDefaultQueuePublisher _defaultQueuePublisher;
    #endregion

    #region Ctor
    public EmailQueuePublisher(IDefaultQueuePublisher defaultQueuePublisher)
    {
        _defaultQueuePublisher = defaultQueuePublisher;
    }
    #endregion

    #region Method(s)
    public async Task Publish(EmailPublisherModel model)
    {
        var queuData = JsonConvert.SerializeObject(model);
        await _defaultQueuePublisher.Publish(Constants.DefaultConstants.EmailQueueName, queuData);
    }
    #endregion
}
