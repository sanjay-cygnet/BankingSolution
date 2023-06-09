﻿namespace BuildingBlocks.EventBus.Services;

using RabbitMQ.Client;

public interface IRabbitMQPersistentConnection : IDisposable
{
    bool IsConnected { get; }
    bool TryConnect();
    bool CheckConnection();
    IModel CreateModel();

    IModel CreateConsumerChannel(string queueName, bool durable = true, bool exclusive = false,
        bool autoDelete = false);
}
