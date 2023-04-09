namespace Notifications.Api.QueueConsumers;

using BuildingBlocks.EventBus.Constants;
using BuildingBlocks.EventBus.Services;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

public class EmailQueueConsumer : BackgroundService
{
    #region Members
    private readonly ILogger<EmailQueueConsumer> _logger;
    private IModel _consumerChannel;
    private readonly IRabbitMQPersistentConnection _persistentConnection;
    #endregion

    #region Ctor
    public EmailQueueConsumer(
        ILogger<EmailQueueConsumer> logger,
        IRabbitMQPersistentConnection persistentConnection)
    {
        _logger = logger;
        _persistentConnection = persistentConnection;
        _persistentConnection = persistentConnection ?? throw new ArgumentNullException(nameof(persistentConnection));
        _consumerChannel = _persistentConnection.CreateConsumerChannel(DefaultConstants.EmailQueueName);
    }

    #endregion

    #region Methods
    /// <summary>
    /// 
    /// </summary>
    /// <param name="stoppingToken"></param>
    /// <returns></returns>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("ConsumeQueue " + DateTime.Now.ToShortTimeString());
        var channel = _persistentConnection.CreateModel();

        channel.QueueDeclare(queue: DefaultConstants.EmailQueueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
        //Consumer
        var consumer = new AsyncEventingBasicConsumer(channel);
        consumer.Received += ConsumerReceivedEvent;

        channel.BasicConsume(queue: DefaultConstants.EmailQueueName, autoAck: true, consumer: consumer);
    }

    private void ConsumeQueue()
    {
        _logger.LogInformation($"Email consume queue started ");
        _consumerChannel.QueueDeclare(queue: DefaultConstants.EmailQueueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
        //Consumer
        var consumer = new AsyncEventingBasicConsumer(_consumerChannel);
        consumer.Received += ConsumerReceivedEvent;

        _consumerChannel.BasicConsume(queue: DefaultConstants.EmailQueueName, autoAck: true, consumer: consumer);
    }

    private async Task ConsumerReceivedEvent(object sender, BasicDeliverEventArgs @eventArgs)
    {
        try
        {
            var body = @eventArgs.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            //var bodyData = JsonConvert.DeserializeObject<SendEmail.Request>(message);
            Console.WriteLine($"\n data received in : {message}");
            ///Here code to call service that sends email
        }
        catch (Exception ex)
        {
            _logger.LogError($"{nameof(EmailQueueConsumer)} => {nameof(ConsumerReceivedEvent)} => Error occured at (UTC)", ex.Message);
        }
    }
    #endregion
}