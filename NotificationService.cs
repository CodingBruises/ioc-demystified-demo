using System;
using System.Text;

namespace IoC_Demystified_Demo
{
    public interface INotificationService
    {
        void SendNotification(string message);
    }

    public class NotificationService : INotificationService
    {
        public void SendNotification(string message)
        {
            var notification = new StringBuilder();
            notification.AppendLine("Sending Message:");
            notification.AppendLine(message);

            Console.WriteLine(notification.ToString());
        }
    }
}
