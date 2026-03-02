namespace Mission08Group13.Models
{
    public interface ITaskItemRepository
    {
        // Get all tasks (including their Category data)
        IQueryable<TaskItem> Tasks { get; }

        // Methods for CRUD operations
        void AddTask(TaskItem task);
        void UpdateTask(TaskItem task);
        void DeleteTask(TaskItem task);
    }
}
