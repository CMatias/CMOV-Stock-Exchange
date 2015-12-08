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

        public async void Initialize()
        {
            pushChannel = await PushNotificationChannelManager.CreatePushNotificationChannelForApplicationAsync();

            Debug.WriteLine(pushChannel.Uri.ToString());
            Debugger.Break();
        }
    }
}
