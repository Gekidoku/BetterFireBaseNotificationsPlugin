using Microsoft.Extensions.Logging;
using Microsoft.Maui.LifecycleEvents;
using Plugin.BetterFirebasePushNotification;

namespace FireBaseSampleApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                })
                .RegisterFirebaseServices();

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
        private static MauiAppBuilder RegisterFirebaseServices(this MauiAppBuilder builder)
        {
            builder.ConfigureLifecycleEvents(events => {
#if IOS
            events.AddiOS(iOS => iOS.FinishedLaunching((_, options) =>
            {
                var usercategories = new NotificationUserCategory[]
            {
                new NotificationUserCategory("MyCategory", new List<NotificationUserAction>
                {
                    new NotificationUserAction("AnswerOne","One", NotificationActionType.Foreground),
                    new NotificationUserAction("AnswerTwo","Two", NotificationActionType.Foreground)

                })
               

            };
                
                FirebasePushNotificationManager.Initialize(options, usercategories, true);
                FirebasePushNotificationManager.SetRateLimit(new TimeSpan(0, 0, 1));
                return false;
            }));

#elif ANDROID
                var usercategories = new NotificationUserCategory[]
                {
                    new NotificationUserCategory("MyCategory", new List<NotificationUserAction>
                    {
                        new NotificationUserAction("AnswerOne","One", NotificationActionType.Foreground),
                        new NotificationUserAction("AnswerTwo","Two", NotificationActionType.Foreground)

                    })


                };

                events.AddAndroid(android => android.OnCreate((activity, bundle) =>
                {

                    FirebasePushNotificationManager.Initialize(usercategories, false, false, true);
                   


                }
                ));




#endif
            });
            return builder;
        }
    }
}
