using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace CodeWarrior.App
{
    [HubName("signalRNotification")]
    public class Notification : Hub
    {
        public void SendMessageByUserId(string userId, string message)
        {
            Clients.User(userId).GetMyMessage(message);
        }
    }
}