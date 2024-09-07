using System.Text;
using System.Text.Json;

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace PosTech.TechChallenge.Contacts.Query.Infra.Workers;

public abstract class ConsumerWorker<T>(ILogger<ConsumerWorker<T>> logger) : BackgroundService
{
    protected readonly ILogger<ConsumerWorker<T>> _logger = logger;
    protected abstract string QueueName { get; }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            var factory = new ConnectionFactory() { HostName = "rabbitmq", Port = 5672, UserName = "guest", Password = "guest" };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare(queue: QueueName,
                             durable: true,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += async (sender, eventArgs) =>
            {
                var body = eventArgs.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var entity = JsonSerializer.Deserialize<T>(body);

                if (entity is not null)
                {
                    await OnMessageReceived(entity, cancellationToken);
                }
                else
                {
                    _logger.LogError("There was a problem when reading message");
                }
            };

            channel.BasicConsume(
                queue: QueueName,
                autoAck: true,
                consumer: consumer);
            await Task.Delay(2000, cancellationToken);
        }
    }

    protected abstract Task OnMessageReceived(T entity, CancellationToken cancellationToken);
}
