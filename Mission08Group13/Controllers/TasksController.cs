using Microsoft.AspNetCore.Mvc;
using Mission08Group13.Models;
using System.Collections.Generic;

namespace Mission08Group13.Controllers
{
    public class TasksController : Controller
    {
      
        public IActionResult Landing()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Quadrants()
        {
            return View();
        }

        private static List<TaskItem> _tasks = new List<TaskItem>();
        private static int _nextId = 1;

        public IActionResult Create(string submittedTask = null)
        {
            ViewBag.SubmittedTask = submittedTask;
            return View();
        }


        [HttpPost]
        public IActionResult Create(TaskItem taskItem)
        {
            if (ModelState.IsValid)
            {
                taskItem.Id = _nextId++;
                _tasks.Add(taskItem);

                return RedirectToAction("Create", new { submittedTask = taskItem.Name });
            }

            return View(taskItem);
        }
    }
}