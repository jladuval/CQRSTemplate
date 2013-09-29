namespace Web.Hubs
{
    using Microsoft.AspNet.SignalR;

    public class EventHub : Hub
    {
        public static void Send(string name)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<EventHub>();
            hubContext.Clients.All.eventStatus(name);
        }

        public static void SendShipObject(string name, string id)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<EventHub>();
            hubContext.Clients.All.shipCreated(name, id);
        }
    }
}