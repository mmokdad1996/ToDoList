using System.ComponentModel.DataAnnotations;

namespace ToDoList.Models
{
    public class UpdateTaskDto
    {
        [Required]
        public string Title { get; set; }

        public string? Description { get; set; }

        public DateTime? DueDate { get; set; }

        [Required]
        public TaskStatus Status { get; set; }
    }

}
