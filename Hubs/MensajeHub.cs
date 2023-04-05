using Microsoft.AspNetCore.SignalR;
namespace FisioFlores.Hubs
{
    public class MensajeHub:Hub
    {
        public async Task SendMessage(string message)
        {
            await Clients.All.SendAsync("grupo1", message);
        }
    }
}
