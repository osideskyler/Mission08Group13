using Microsoft.AspNetCore.Mvc;
using Mission08Group13.Models;
using Microsoft.EntityFrameworkCore; 
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using SQLitePCL;

namespace Mission08Group13.Controllers
{
    public class TasksController : Controller
    {
        private TaskItemContext _context;

        // Change TaskContext to TaskItemContext here as well
        public TasksController(TaskItemContext context)
        {
            _context = context;
        }
      
        public IActionResult Landing()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Quadrants()
        {
            var tasks = _context.Tasks
                .Include(x => x.Category)
                .Where(x => x.Completed == false)
                .ToList();

            return View(tasks);
        }

        
        [HttpGet]
        public IActionResult Create(string submittedTask = null)
        {
            ViewBag.SubmittedTaskItem = submittedTask;
            ViewBag.Categories = new SelectList(_context.Categories, "CategoryId", "CategoryName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TaskItem taskItem)
        {
            if (ModelState.IsValid)
            {
                _context.Tasks.Add(taskItem);
                _context.SaveChanges();

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