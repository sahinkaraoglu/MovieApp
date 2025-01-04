using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace MovieApp.Api.Services.RabbitMQ
{
    public interface IRabbitMQService
    {
        void SendMessage<T>(T message, string queueName);
    }

    public class RabbitMQService : IRabbitMQService
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private const string QUEUE_NAME = "comments_queue";

        public RabbitMQService()
        {
            try
            {
                var factory = new ConnectionFactory
                {
                    HostName = "localhost",
                    Port = 5672,      
                    UserName = "guest",
                    Password = "guest",
                    VirtualHost = "/",
                    RequestedHeartbeat = TimeSpan.FromSeconds(30),
                    AutomaticRecoveryEnabled = true  
                };

                Console.WriteLine("RabbitMQ bağlantısı kuruluyor...");
                _connection = factory.CreateConnection();
                Console.WriteLine("RabbitMQ bağlantısı başarılı!");

                _channel = _connection.CreateModel();
                Console.WriteLine("RabbitMQ channel oluşturuldu!");

                _channel.QueueDeclare(
                    queue: QUEUE_NAME,
                    durable: true,     
                    exclusive: false,   
                    autoDelete: false,  
                    arguments: null
                );
                Console.WriteLine($"'{QUEUE_NAME}' kuyruğu oluşturuldu!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"RabbitMQ bağlantı hatası: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                throw;
            }
        }

        public void SendMessage<T>(T message, string queueName)
        {
            var jsonMessage = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(jsonMessage);

            var properties = _channel.CreateBasicProperties();
            properties.Persistent = true;

            _channel.BasicPublish(
                exchange: "",
                routingKey: queueName,
                basicProperties: properties,
                body: body
            );

            Console.WriteLine($"Mesaj gönderildi: {jsonMessage}");
        }
    }
} 