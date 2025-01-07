using System.Collections.Generic;
using Android.Content;

namespace Plugin.BetterFirebasePushNotification
{ 
    [BroadcastReceiver(Enabled = true, Exported = false)]
    public class PushNotificationReadReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            var action = intent.Action;


            IDictionary<string, object> parameters = new Dictionary<string, object>();
            var extras = intent.Extras;

            if (extras != null && !extras.IsEmpty)
            {
                foreach (var key in extras.KeySet())
                {
                    parameters.Add(key, $"{extras.Get(key)}");
                    System.Diagnostics.Debug.WriteLine(key, $"{extras.Get(key)}");
                }
            }
            if(action == "ACTION_ACCEPT")
            {
                FirebasePushNotificationManager.RegisterAccept(parameters);
            }
            else if(action == "ACTION_DECLINE")
            { 
                FirebasePushNotificationManager.RegisterReject(parameters); 
            }
            
        }
    }
}
