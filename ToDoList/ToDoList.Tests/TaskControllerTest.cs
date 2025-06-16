using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;
namespace ToDoList.ToDoList.Tests
{


   
    public class TaskControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public TaskControllerTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetTasks_ShouldReturnSuccess()
        {
            var newTask = new { Name = "New Task" };
            var content = new StringContent(JsonSerializer.Serialize(newTask), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("http://localhost:8080/api/tasks", content);
            var responseBody = await response.Content.ReadAsStringAsync();

            Assert.True(response.IsSuccessStatusCode, "Task creation should return a success status.");
            Assert.Contains("Task created successfully", responseBody);
        }
    }


}
