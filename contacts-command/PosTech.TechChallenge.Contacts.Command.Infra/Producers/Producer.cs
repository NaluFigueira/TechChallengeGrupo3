using System.Text;
using System.Text.Json;

using PosTech.TechChallenge.Contacts.Command.Infra.Interfaces;

using RabbitMQ.Client;

namespace PosTech.TechChallenge.Contacts.Command.Infra.Producers;

public class Producer : IProducer
{
    public void PublishMessageOnQueue<T>(T messageBody, string queueName)
    {
        var factory = new ConnectionFactory() { HostName = "rabbitmq", Port = 5672, UserName = "guest", Password = "guest" };
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.QueueDeclare(
            queue: queueName,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        string message = JsonSerializer.Serialize(messageBody);
        var body = Encoding.UTF8.GetBytes(message);

        channel.BasicPublish(
            exchange: "",
            routingKey: queueName,
            basicProperties: null,
            body);
    }
}
