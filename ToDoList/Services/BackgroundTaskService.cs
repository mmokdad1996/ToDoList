using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;
namespace ToDoList.Services
{
   

    public class BackgroundTaskService : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                Console.WriteLine($"Background task running at {DateTime.Now}");
                await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken); // ✅ Runs every 10 seconds
            }
        }
    }

}
