using BuildingBlocks.EventBus.Services;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System.Text;

namespace BuildingBlocks.EventBus.QueuePublisher
{
    internal sealed class DefaultQueuePublisher : IDefaultQueuePublisher
    {
        #region Members
        private IModel _consumerChannel;

        private ILogger<DefaultQueuePublisher> _logger;

        private readonly IRabbitMQPersistentConnection _persistentConnection;
        #endregion

        #region Ctor
        public DefaultQueuePublisher(
            ILogger<DefaultQueuePublisher> logger,
            IRabbitMQPersistentConnection persistentConnection)
        {
            _logger = logger;
            _persistentConnection = persistentConnection;
            _persistentConnection = persistentConnection ?? throw new ArgumentNullException(nameof(persistentConnection));
        }
        #endregion

        #region Method(s)
        public async Task Publish(string queueName, string queuData)
        {
            try
            {
                _consumerChannel = _persistentConnection.CreateConsumerChannel(queueName);
                var body = Encoding.UTF8.GetBytes(queuData);
                _consumerChannel.BasicPublish(exchange: "",
                    routingKey: queueName,
                    basicProperties: null,
                    body: body);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(DefaultQueuePublisher)} => {nameof(Publish)} , error occcured {ex.Message}");
            }
        }
        #endregion
    }
}
