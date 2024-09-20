using Microsoft.AspNetCore.SignalR;

namespace BetaLogistics.Hubs
{
    public class TrackingHub : Hub
    {
        public async Task SendLocationUpdate(int shipmentId, string latitude, string longitude)
        {
            await Clients.All.SendAsync("ReceiveLocationUpdate", shipmentId, latitude, longitude);
        }
    }
}
