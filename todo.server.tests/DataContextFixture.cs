using Microsoft.EntityFrameworkCore;
using System;
using todo.server.Data;

namespace todo.server.tests
{
    public class DataContextFixture : IDisposable
    {
        public DataContext DataContext { get; private set; }

        public DataContextFixture()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "InMem")
                .Options;

            DataContext = new DataContext(options);
            DataContext.TodoTasks!.AddRange(TestDataProvider.GetTasksEntityData());
            DataContext.SaveChanges();
        }

        public void Dispose()
        {
            // Clean up resources if needed
            DataContext.Dispose();
        }
    }
}
