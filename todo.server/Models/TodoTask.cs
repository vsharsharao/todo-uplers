using System.ComponentModel.DataAnnotations;
using todo.server.Data.Entities;

namespace todo.server.Models
{
    public class TodoTask
    {
        public long Id { get; set; }

        [Required]
        public string? Title { get; set; }

        [Required]
        public string? Description { get; set; }
        public Priority Priority { get; set; } = Priority.Low;
        public bool IsCompleted { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime UpdatedOn { get; set; } = DateTime.Now;
        
        public TodoTask()
        {
               
        }

        public TodoTask(string title, string description)
        {
            Title = title;
            Description = description;
        }

        public TodoTask(TodoTaskData taskData)
        {
            Id = taskData.Id;
            Title = taskData.Title!;
            Description = taskData.Description!;
            Priority = taskData.Priority;
            IsCompleted = taskData.IsCompleted;
            CreatedOn = taskData.CreatedOn;
            UpdatedOn = taskData.UpdatedOn;
        }
    }

    public enum Priority
    {
        Low,
        Medium,
        High
    }
}