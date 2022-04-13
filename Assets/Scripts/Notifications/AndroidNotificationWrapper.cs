using System;
using System.Collections;
using Unity.Notifications.Android;

namespace Notifications
{
    internal class AndroidNotificationWrapper : INotificationWrapper
    {
        public AndroidNotificationWrapper(string[] channels)
        {
            foreach (var channel in channels)
            {
                AndroidNotificationCenter.RegisterNotificationChannel(new AndroidNotificationChannel()
                {
                    Id = channel,
                    Name = channel,
                    Importance = Importance.Default
                });
            }
        }
        public void ClearNotifications()
        {
            AndroidNotificationCenter.CancelAllNotifications();
        }

        public void RegisterNotification(string title, string body, DateTime fireTime, string channel)
        {
            AndroidNotificationCenter.SendNotification(new AndroidNotification()
            {
                Title = title,
                Text = body,
                FireTime = fireTime
            }, channel);
        }

        public IEnumerator RequestAuthorization()
        {
            // Do nothing
            return null;
        }
    }
}