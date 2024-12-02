using System;
using Azure.Storage.Queues.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Company.Function
{
    public class StudentFunctions
    {
        private readonly ILogger<StudentFunctions> _logger;

        public StudentFunctions(ILogger<StudentFunctions> logger)
        {
            _logger = logger;
        }

        [Function(nameof(StudentFunctions))]
        public void Run([QueueTrigger("student-queue", Connection = "lechungdung_STORAGE")] QueueMessage message)
        {
            _logger.LogInformation($"C# Queue trigger function processed: {message.MessageText}");
        }
    }
}
