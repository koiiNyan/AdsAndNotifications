using System;
using System.Collections;
using Unity.Notifications.iOS;
using UnityEngine;

namespace Notifications
{
    internal class iOSNotificationWrapper : INotificationWrapper
    {
        public void ClearNotifications()
        {
            iOSNotificationCenter.RemoveAllDeliveredNotifications();
            iOSNotificationCenter.RemoveAllScheduledNotifications();
        }

        public void RegisterNotification(string title, string body, DateTime fireTime, string channel)
        {
            iOSNotificationCenter.ScheduleNotification(new iOSNotification()
            {
                Title = title,
                Body = body, 
                ShowInForeground = true,
                ForegroundPresentationOption = PresentationOption.Alert | PresentationOption.Sound,
                CategoryIdentifier = channel,
                Trigger = new iOSNotificationCalendarTrigger
                {
                    Year = fireTime.Year,
                    Month = fireTime.Month,
                    Day = fireTime.Day,
                    Hour = fireTime.Hour,
                    Minute = fireTime.Minute,
                    Second = fireTime.Second,
                    Repeats = false
                }
            });
        }

        public IEnumerator RequestAuthorization()
        {
            var option = AuthorizationOption.Alert | AuthorizationOption.Sound;
            using (var req = new AuthorizationRequest(option, false))
            {
                while (!req.IsFinished)
                {
                    yield return null;
                }
                //
                Debug.Log("Result: " + req.IsFinished);
            }
        }
    }
}