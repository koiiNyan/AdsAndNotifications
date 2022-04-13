using System;
using System.Collections;

namespace Notifications
{
    internal interface INotificationWrapper
    {
        void RegisterNotification(string title, string body, DateTime fireTime, string channel);

        void ClearNotifications();

        // For IOS mandatory
        IEnumerator RequestAuthorization();
    }
}