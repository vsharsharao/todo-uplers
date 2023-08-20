using Microsoft.EntityFrameworkCore;
using todo.server.Data.Entities;

namespace todo.server.Data
{
    public class LocalDataContext : DataContext
    {
        public LocalDataContext(DbContextOptions options) : base(options)
        {
        }
        public override DbSet<TodoTaskData>? TodoTasks { get; set; }
    }
}
