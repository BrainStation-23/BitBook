using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using CodeWarrior.App.RealTimeNotification;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace CodeWarrior.App
{
    [HubName("signalRNotification")]
    [Authorize]
    public class Notification : Hub
    {
        private readonly static ConnectionMapping<string> Connections =
            new ConnectionMapping<string>();
        public void SendMessageByUserId(string userId)
        {
           // Clients.User(userId).SendUserNotification("Click On Your Question.");
            Clients.Group(userId).SendUserNotification("Click On Your Question.");
        }
        public void AddQuestionNotification(string question,string userName)
        {
            Clients.All.NewQuestionAdded(question,userName);
        }

        public override Task OnConnected()
        {
            string name = Context.User.Identity.Name;

            Groups.Add(Context.ConnectionId, name);

            return base.OnConnected();
        }

        //public override Task OnDisconnected()
        //{
        //    string name = Context.User.Identity.Name;

        //    Connections.Remove(name, Context.ConnectionId);

        //    return base.OnDisconnected();
        //}

        //public override Task OnReconnected()
        //{
        //    string name = Context.User.Identity.Name;

        //    if (!Connections.GetConnections(name).Contains(Context.ConnectionId))
        //    {
        //        Connections.Add(name, Context.ConnectionId);
        //    }

        //    return base.OnReconnected();
        //}
    }
}