using Microsoft.AspNetCore.Mvc;
using Mission08Group13.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using SQLitePCL;

namespace Mission08Group13.Controllers
{
    public class TasksController : Controller
    {
        private TaskContext _context;

        public TasksController(TaskContext context)
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
            return View();
        }

        
        [HttpGet]
        public IActionResult Create(string submittedTask = null)
        {
            ViewBag.SubmittedTaskItem = submittedTask;
            ViewBag.Categories = new SelectList(_context.Categories, "CategoryId", "CategoryName");
            return View();
        }

        [HttpPost]
        public IActionResult Create(TaskItem taskItem)
        {
            if (ModelState.IsValid)
            {
                _context.Tasks.Add(taskItem);
                _context.SaveChanges();

                return RedirectToAction("Create", new { submittedTask = taskItem.Name });
            }

            return View(taskItem);
        }
    }
}