using todo.server.Models;

namespace todo.server.Services.Interfaces
{
    public interface ITodoService
    {
        public IEnumerable<TodoTask> GetTasks();
        public TodoTask? GetTaskById(long id);
        public Task<TodoTask> CreateTaskAsync(TodoTask task);
        public Task<TodoTask?> UpdateTaskAsync(TodoTask task);
        public Task<TodoTask?> UpdateAsCompletedAsync(long id, bool isCompleted);
        public Task<IEnumerable<TodoTask>?> UpdateAsCompletedAsync(IEnumerable<long> taskIds);
        public Task DeleteTaskAsync(long id);
        public Task DeleteTaskAsync(IEnumerable<long> taskIds);
    }
}
