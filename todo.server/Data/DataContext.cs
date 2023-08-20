using Microsoft.EntityFrameworkCore;
using todo.server.Data.Entities;

namespace todo.server.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }
        public virtual DbSet<TodoTaskData>? TodoTasks { get; set; }
    }
}
