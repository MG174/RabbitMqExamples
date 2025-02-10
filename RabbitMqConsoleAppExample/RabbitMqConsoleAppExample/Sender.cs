using RabbitMQ.Client;
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

    var message = "Sample string message";

    var body = Encoding.UTF8.GetBytes( message );

    await channel.BasicPublishAsync("", routeKey, body);
    Console.WriteLine($"Sent message to {routeKey}. Content: {message}.");
    Console.ReadLine();
}
