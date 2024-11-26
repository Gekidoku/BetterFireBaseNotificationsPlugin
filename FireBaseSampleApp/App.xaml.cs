using Plugin.BetterFirebasePushNotification;

namespace FireBaseSampleApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
            BetterFirebasePushNotification.Current.OnTokenRefresh += async (s, p) =>
            {
                var MyToken = p.Token;
                //Your reg token is now in MyToken

            };
            BetterFirebasePushNotification.Current.OnNotificationReceived += async (s, p) =>
            {
               // Do something with the Data keys in the notification

            };
            BetterFirebasePushNotification.Current.OnNotificationAction += async (s, p) =>
            {
                //Do something based on what button was pressed on the notification action
                if (!string.IsNullOrEmpty(p.Identifier))
                {
                    if (p.Identifier == "AnswerOne")
                    {

                    }else if(p.Identifier == "AnswerTwo")
                    {

                    }
                }
                    
            };
        }
    }
}
