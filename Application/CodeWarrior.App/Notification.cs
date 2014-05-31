using System.Threading.Tasks;
using System.Web;
using CodeWarrior.App.RealTimeNotification;
using CodeWarrior.Model;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace CodeWarrior.App
{
    [HubName("signalRNotification")]
    public class Notification : Hub
    {
        public void SendMessageByUserId(string userId)
        {
            // Clients.User(userId).SendUserNotification("Click On Your Question.");
            Clients.All.SendUserNotification("Click On Your Question.", userId);
        }

        public void AddQuestionNotification(string question, string userName)
        {
            Clients.All.NewQuestionAdded(question, userName);
        }

        public void LikeInMyPost(string message, string userName)
        {
            Clients.Others.MyNotification(message, userName);
        }

        public void CommentInMyPost(string message, string userName)
        {
            Clients.Others.MyNotification(message, userName);
        }

        public void NewPostAdded(object post, string[] users)
        {
            Clients.Others.UpdateMyFeed(post, users);
        }

        public void AddFriendNotification(string message, string userName)
        {
            Clients.Others.MyNotification(message, userName);
        }
    }

}