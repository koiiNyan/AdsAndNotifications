using System;
using UnityEngine;

namespace Notifications
{
    public class NotificationsManager : MonoBehaviour
    {
        [SerializeField]
        private string[] channels = { "default" };

        private INotificationWrapper _wrapper;

        private void Awake()
        {
            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                _wrapper = new iOSNotificationWrapper();
            }
            else // if platform is android
            {
                _wrapper = new AndroidNotificationWrapper(channels);
            }

            _wrapper.ClearNotifications();
            DontDestroyOnLoad(this.gameObject);
            _wrapper.RequestAuthorization();
        }

        private void OnApplicationPause(bool pause)
        {
            if (pause)
            {
                RegisterNotifications();
            }
            else
            {
                _wrapper.ClearNotifications();
            }
        }

        private void RegisterNotifications()
        {
            // calculate
            _wrapper.RegisterNotification("title", "body", DateTime.Now.AddMinutes(1), channels[0]);
        }
    }
}
