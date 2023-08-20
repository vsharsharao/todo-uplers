using System.Collections.Generic;
using todo.server.Data.Entities;
using todo.server.Models;

namespace todo.server.tests
{
    internal static class TestDataProvider
    {
        public static IEnumerable<TodoTask> GetTasks()
        {
            return new List<TodoTask>()
            {
                new()
                {
                    Id = 1,
                    Title = "Books to read",
                    Description = "Few titles to read, update this list sooner.",
                    Priority = Priority.Medium
                },
                new()
                {
                    Id = 2,
                    Title = "Movies to watch",
                    Description = "Few titles to watch, update this list sooner.",
                    Priority = Priority.Low
                },
                new()
                {
                    Id = 3,
                    Title = "Grocery list",
                    Description = "Grocery list, update this list sooner.",
                    Priority = Priority.High
                }
            };
        }

        public static IEnumerable<TodoTaskData> GetTasksEntityData()
        {
            return new List<TodoTaskData>()
            {
                new()
                {
                    Id = 1,
                    Title = "Books to read",
                    Description = "Few titles to read, update this list sooner.",
                    Priority = Priority.Medium
                },
                new()
                {
                    Id = 2,
                    Title = "Movies to watch",
                    Description = "Few titles to watch, update this list sooner.",
                    Priority = Priority.Low
                },
                new()
                {
                    Id = 3,
                    Title = "Grocery list",
                    Description = "Grocery list, update this list sooner.",
                    Priority = Priority.High
                },
                new()
                {
                    Id = 4,
                    Title = "Shopping list",
                    Description = "Shopping list, update this list sooner.",
                    Priority = Priority.High
                },
                new()
                {
                    Id = 5,
                    Title = "Scheduler",
                    Description = "Scheduler here",
                    Priority = Priority.High
                }
            };
        }
    }
}
