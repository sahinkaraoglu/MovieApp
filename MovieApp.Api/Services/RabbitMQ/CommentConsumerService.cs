using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using MovieApp.Domain.Entity;
using MovieApp.Infrastructure.Context;

namespace MovieApp.Api.Services.RabbitMQ
{
    public class CommentConsumerService : BackgroundService
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly IServiceProvider _serviceProvider;
        private const string QUEUE_NAME = "comments_queue";

        public CommentConsumerService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                Port = 5672,
                UserName = "guest",
                Password = "guest"
            };

            try
            {
                _connection = factory.CreateConnection();
                _channel = _connection.CreateModel();

                _channel.QueueDeclare(
                    queue: QUEUE_NAME,
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null
                );

                Console.WriteLine("Consumer başarıyla başlatıldı!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Consumer başlatma hatası: {ex.Message}");
                throw;
            }
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += async (model, ea) =>
            {
                try
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    var comment = JsonSerializer.Deserialize<Comment>(message);

                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var dbContext = scope.ServiceProvider.GetRequiredService<MovieDbContext>();
                        await dbContext.Comments.AddAsync(comment);
                        await dbContext.SaveChangesAsync();
                        Console.WriteLine($"Yorum kaydedildi: {comment.Text}");
                    }

                    _channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Mesaj işleme hatası: {ex.Message}");
                }
            };

            _channel.BasicConsume(
                queue: QUEUE_NAME,
                autoAck: false,
                consumer: consumer
            );

            return Task.CompletedTask;
        }

        public override void Dispose()
        {
            _channel?.Dispose();
            _connection?.Dispose();
            base.Dispose();
        }
    }
} 