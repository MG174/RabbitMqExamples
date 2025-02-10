using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

var factory = new ConnectionFactory() { HostName = "localhost" };

var routeKey = "BasicTest";

using (var connection = await factory.CreateConnectionAsync())
using (var channel = await connection.CreateChannelAsync())
{
    await channel.QueueDeclareAsync(
        routeKey,
        false,
        false,
        false,
        null
        );

    var consumer = new AsyncEventingBasicConsumer(channel);

    consumer.ReceivedAsync += async (model, ea) =>
    {
        var body = ea.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);
        Console.WriteLine($"Received message: {message}");
    };

    await channel.BasicConsumeAsync(routeKey, true, consumer);

    Console.ReadLine();
}