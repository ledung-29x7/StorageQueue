using Azure.Storage.Queues;
using System.Text.Json;

namespace StudentMVC.Services
{
    public class QueueService
    {

        private readonly QueueClient _queueClient;

        public QueueService(IConfiguration configuration)
        {
            var connectionString = configuration["AzureStorageQueue:ConnectionString"];
            var queueName = configuration["AzureStorageQueue:QueueName"];
            _queueClient = new QueueClient(connectionString, queueName);
            _queueClient.CreateIfNotExists();
        }

        public async Task SendMessageAsync<T>(T message)
        {
            var messageString = JsonSerializer.Serialize(message);
            Console.WriteLine($"Message sent: {messageString}"); // Log ra console
            await _queueClient.SendMessageAsync(messageString);
        }

    }
}
