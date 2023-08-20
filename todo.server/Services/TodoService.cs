using todo.server;
using todo.server.Data.Entities;
using todo.server.Data.Interfaces;
using todo.server.Services.Interfaces;
using todo.server.Models;

public class TodoService : ITodoService
{
    private readonly IRepository<TodoTaskData> _todoRepo;
    public TodoService(IRepository<TodoTaskData> todoRepo)
    {
        _todoRepo = todoRepo;
    }
    public async Task<TodoTask> CreateTaskAsync(TodoTask task)
    {
        var createdTask = await _todoRepo.CreateAsync(new()
        {
            Title = task.Title,
            Description = task.Description,
            Priority = task.Priority,
            CreatedOn = task.CreatedOn,
            UpdatedOn = task.UpdatedOn
        });

        return new(createdTask);
    }

    public IEnumerable<TodoTask> GetTasks()
    {
        return _todoRepo.Get().Select(i => new TodoTask(i));
    }

    public TodoTask? GetTaskById(long id)
    {
        return _todoRepo.Get(i => i.Id == id).Select(i => new TodoTask(i)).FirstOrDefault();
    }

    public async Task<TodoTask?> UpdateTaskAsync(TodoTask task)
    {
        var _task = _todoRepo.Get(i => i.Id == task.Id).FirstOrDefault();

        if (_task == null)
            return null;

        _task.Title = task.Title;
        _task.Description = task.Description;
        _task.Priority = task.Priority;
        _task.UpdatedOn = task.UpdatedOn;
        _task.IsCompleted = task.IsCompleted;

        var updatedTask = await _todoRepo.UpdateAsync(_task);

        return new(updatedTask);
    }

    public async Task<TodoTask?> UpdateAsCompletedAsync(long id, bool isCompleted)
    {
        var _task = _todoRepo.Get(i => i.Id == id).FirstOrDefault();
        
        if (_task == null)
            return null;

        _task.IsCompleted = isCompleted;
        _task.UpdatedOn = DateTime.Now;

        var updatedTask = await _todoRepo.UpdateAsync(_task);
        return new(updatedTask);
    }

    public async Task<IEnumerable<TodoTask>?> UpdateAsCompletedAsync(IEnumerable<long> taskIds)
    {
        var _tasks = _todoRepo.Get(i => taskIds.Contains(i.Id));

        if (!_tasks.Any())
            return null;

        foreach (var task in _tasks)
        {
            task.IsCompleted = true;
            task.UpdatedOn = DateTime.Now;
        }

        var updatedTasks = await _todoRepo.UpdateRangeAsync(_tasks);
        return updatedTasks.Select(i => new TodoTask(i));
    }

    public async Task DeleteTaskAsync(long id)
    {
        var _task = _todoRepo.Get(i => i.Id == id).FirstOrDefault();

        if (_task == null)
            return;

        await _todoRepo.DeleteAsync(_task);
    }

    public async Task DeleteTaskAsync(IEnumerable<long> taskIds)
    {
        var _tasks = _todoRepo.Get(i => taskIds.Contains(i.Id));

        if (!_tasks.Any())
            return;

        await _todoRepo.DeleteRangeAsync(_tasks);
    }
}