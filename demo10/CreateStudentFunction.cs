using System;
using System.Text.Json;
using demo10.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace demo10;

public class CreateStudentFunction
{
    private readonly StudentDbContext _dbContext;

    public CreateStudentFunction(StudentDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [Function("CreateStudent")]
    public async Task Run([QueueTrigger("student-queue", Connection = "lechungdung_STORAGE")] string studentQueueItem, ILogger log)
    {
        try
        {
            var student = JsonSerializer.Deserialize<Student>(studentQueueItem);
            if (student != null)
            {
                var name_param = new SqlParameter("@Name", student.Name);
                var age_param = new SqlParameter("@Age", student.Age);
                await _dbContext.Database.ExecuteSqlRawAsync("EXECUTE dbo.CreateStudent @Name, @Age", name_param, age_param);
                // _dbContext.Student.Add(student);
                // await _dbContext.SaveChangesAsync();

                log.LogInformation($"Student created: {student.Name}");
            }
        }
        catch (Exception ex)
        {
            log.LogError($"Error processing queue item: {ex.Message}");
        }
    }

    [Function("GetAllStudent")]
    public async Task<IActionResult> GetAllStudent(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "students")] HttpRequest req)
    {
        var students = await _dbContext.Student.ToListAsync();

        return new OkObjectResult(students);
    }

}
