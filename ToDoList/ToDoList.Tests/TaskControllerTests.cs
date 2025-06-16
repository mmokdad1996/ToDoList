using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

public class TaskControllerTests
{
    private readonly HttpClient _client;

    public TaskControllerTests()
    {
        var server = new TestServer(new WebHostBuilder().UseStartup<IStartup>());
        _client = server.CreateClient();
    }

    [Fact]
    public async Task CreateTask_ShouldReturnSuccess()
    {
        var newTask = new { name = "New Task" };
        var content = new StringContent(JsonSerializer.Serialize(newTask), Encoding.UTF8, "application/json");

        var response = await _client.PostAsync("/api/tasks", content);

        Assert.True(response.IsSuccessStatusCode, "Task creation should return a success status.");
    }
}
