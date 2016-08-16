using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking.PushNotifications;
using System.Diagnostics;

namespace SharedPush
{
    public class SetupPush
    {
        private PushNotificationChannel pushChannel;
        public string uri;
        public async void Initialize()
        {
            pushChannel = await PushNotificationChannelManager.CreatePushNotificationChannelForApplicationAsync();

            uri = pushChannel.Uri.ToString();
        }

        public string getUri()
        {
            return uri;
        }
    }
}
