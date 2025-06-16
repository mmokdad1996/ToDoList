using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
namespace ToDoList.Hubs
{
   

 
        public class ChatHub : Hub
        {
        public async Task ChangeButtonColor(string color)
        {
            await Clients.All.SendAsync("UpdateButtonColor", color); // ✅ Sends color update to all clients
        }
    }
    

}
