using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using todo.server.Models;

namespace todo.server.Data.Entities
{
    // Commented DatabaseGenerated attribute, this is not supported in sqlite library
    public class TodoTaskData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public Priority Priority { get; set; }
        public bool IsCompleted { get; set; }

        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedOn { get; set; }

        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdatedOn { get; set; }
    }
}
