using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoList.Data;
using ToDoList.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoList.Controllers
{
    [ApiController]
    [Route("api/tasks")]
    public class TaskController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TaskController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetTasks()
        {
            var tasks = await _context.Tasks.ToListAsync();
            return Ok(tasks);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] TaskItem newTask)
        {
            if (newTask == null || string.IsNullOrEmpty(newTask.Title))
            {
                return BadRequest("Title is required.");
            }

            newTask.Id = Guid.NewGuid();
            newTask.CreatedDate = DateTime.UtcNow;

            _context.Tasks.Add(newTask);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Task created successfully", task = newTask });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskItem>> GetTaskById(Guid id)
        {
            var task = await _context.Tasks.FindAsync(id);
            return task == null ? NotFound() : Ok(task);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(Guid id, [FromBody] UpdateTaskDto taskDto)
        {
            var existingTask = await _context.Tasks.FindAsync(id);
            if (existingTask == null) return NotFound();

            existingTask.Title = taskDto.Title;
            existingTask.Description = taskDto.Description;
            existingTask.DueDate = taskDto.DueDate;
            existingTask.Status = taskDto.Status;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(Guid id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null) return NotFound();

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
