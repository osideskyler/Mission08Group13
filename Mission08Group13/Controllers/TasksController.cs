using Microsoft.AspNetCore.Mvc;
using Mission08Group13.Models;
using System.Collections.Generic;
using System.Linq;

namespace Mission08Group13.Controllers
{
    public class TasksController : Controller
    {
        private static List<TaskItem> _tasks = new List<TaskItem>();
        private static int _nextId = 1;

        public IActionResult Landing()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Quadrants()
        {
            var incomplete = _tasks.Where(t => !t.Completed).ToList();
            return View(incomplete);
        }

        [HttpGet]
        public IActionResult Create(int? id, string submittedTask = null)
        {
            ViewBag.SubmittedTask = submittedTask;
            if (id.HasValue)
            {
                var task = _tasks.FirstOrDefault(t => t.Id == id.Value);
                if (task != null)
                    return View(task);
                return RedirectToAction(nameof(Quadrants));
            }
            return View(new TaskItem());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TaskItem taskItem)
        {
            if (ModelState.IsValid)
            {
                if (taskItem.Id > 0)
                {
                    var existing = _tasks.FirstOrDefault(t => t.Id == taskItem.Id);
                    if (existing != null)
                    {
                        existing.Name = taskItem.Name;
                        existing.DueDate = taskItem.DueDate;
                        existing.Quadrant = taskItem.Quadrant;
                        existing.Category = taskItem.Category;
                        existing.Completed = taskItem.Completed;
                        return RedirectToAction(nameof(Quadrants));
                    }
                }
                taskItem.Id = _nextId++;
                _tasks.Add(taskItem);
                return RedirectToAction("Create", new { submittedTask = taskItem.Name });
            }
            return View(taskItem);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Complete(int id)
        {
            var task = _tasks.FirstOrDefault(t => t.Id == id);
            if (task != null)
                task.Completed = true;
            return RedirectToAction(nameof(Quadrants));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var task = _tasks.FirstOrDefault(t => t.Id == id);
            if (task != null)
                _tasks.Remove(task);
            return RedirectToAction(nameof(Quadrants));
        }
    }
}