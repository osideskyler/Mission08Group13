using Microsoft.EntityFrameworkCore;

namespace Mission08Group13.Models
{
    public class EFTaskItemRepository : ITaskItemRepository
    {
        private TaskItemContext _context;

        public EFTaskItemRepository(TaskItemContext temp)
        {
            _context = temp;
        }

        // Implementation of the Tasks getter, including the Category join
        public IQueryable<TaskItem> Tasks => _context.Tasks.Include(x => x.Category);

        public void AddTask(TaskItem task)
        {
            _context.Tasks.Add(task);
            _context.SaveChanges();
        }

        public void UpdateTask(TaskItem task)
        {
            _context.Tasks.Update(task);
            _context.SaveChanges();
        }

        public void DeleteTask(TaskItem task)
        {
            _context.Tasks.Remove(task);
            _context.SaveChanges();
        }
    }
    
}
