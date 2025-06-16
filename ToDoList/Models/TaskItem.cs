using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ToDoList.Models
{


    public class TaskItem
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime? DueDate { get; set; }
        public TaskStatus Status { get; set; } = TaskStatus.Pending;
        public Guid CategoryId { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
    public enum TaskStatus
    {
        Pending,
        Completed,
        Cancelled
    }


}
