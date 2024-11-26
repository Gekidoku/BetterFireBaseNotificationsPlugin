using Android.App;
using Android.Content.PM;
using Android.Media;
using Android.OS;
using Plugin.BetterFirebasePushNotification;

namespace FireBaseSampleApp
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            

            base.OnCreate(bundle);
            CreateNotificationChannelIfNeeded();

           

        }
        private void CreateNotificationChannelIfNeeded()
        {
            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                FirebasePushNotificationManager.DefaultNotificationChannelId = "MyChannel";
                var notificationchannel = new NotificationChannel("MyChannel", "myChannel", NotificationImportance.Max);

                //You can add a sound file to Android > Resources > Raw and add it to the channel to change what sound plays on notification
                //var sounduri = Android.Net.Uri.Parse("android.resource://" + PackageName + "/" + FireBaseSampleApp.Resource.Raw.horn);
               // var type = new AudioAttributes.Builder().SetContentType(AudioContentType.Sonification).SetUsage(AudioUsageKind.Notification).Build();
                //notificationchannel.SetSound(sounduri, type);

                var manager = (NotificationManager)GetSystemService(NotificationService);

                manager.CreateNotificationChannel(notificationchannel);
               
                //Add an icon to Resource > Drawable and you can add it here to change the icon thats used with notifications.
               // FirebasePushNotificationManager.IconResource = TRNSPRNTBeveiligerAppMaui.Resource.Drawable.iconnotification;
               
            }
        }
    }
}
