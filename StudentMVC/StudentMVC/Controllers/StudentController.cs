using Microsoft.AspNetCore.Mvc;
using StudentMVC.Models;
using StudentMVC.Services;

namespace StudentMVC.Controllers
{
    public class StudentController : Controller
    {
        private readonly QueueService _queueService;

        public StudentController(QueueService queueService)
        {
            _queueService = queueService;
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(Student student)
        {
            if (ModelState.IsValid)
            {
                await _queueService.SendMessageAsync(student);
                TempData["Message"] = "Student added to queue successfully!";
                return RedirectToAction(nameof(Create));
            }

            return View(student);
        }

    }
}
