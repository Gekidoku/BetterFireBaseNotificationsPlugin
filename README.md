# BetterFireBaseNotificationsPlugin
Attempting to upgrade CrossFireBasePlugin to MAUI

Been using https://github.com/CrossGeeks/FirebasePushNotificationPlugin for my Xamarin app.
this doesnt work for MAUI .net7 so im trying to upgrade it to maui .net7 

# So far so good
For now this project builds and can be added to a solution.
If you want to use it as is just clone it and add it to your solution.
then in your app go to dependencies and add it as a project dependency.

You may need to add 
 <ItemGroup Condition="'$(TargetFramework)' == 'net7.0-android33.0'">
    <PackageReference Include="Xamarin.AndroidX.Activity" Version="1.7.2.1" />
    <PackageReference Include="Xamarin.AndroidX.Activity.Ktx" Version="1.7.2.1" />
  </ItemGroup>
  to your project.csproj file

# What works so far in Android
- [x] Basic Notifications <br/>
- [x] Notifications with Data<br/>
- [x] Notifications with user actions like in in the crossgeeks library<br/>
- [x] Silent notifications in foreground and background<br/>
- [ ] Notifications when the app is closed/killed. still have to test this / start on it.<br/>
# What works so far in iOS
- [x] Nothing
Havent started testing with ios yet. So nothing is confirmed.


# My app project
My app is a default maui app without blazor
```
 <TargetFrameworks>net7.0-android33.0;net7.0-ios</TargetFrameworks>  
    <UseMaui>true</UseMaui>
    <SingleProject>true</SingleProject>
    <ImplicitUsings>enable</ImplicitUsings>
<OutputType>Exe</OutputType>
```
 These are my other references
```
 <ItemGroup Condition="'$(TargetFramework)' == 'net7.0-android33.0'">
    <PackageReference Include="Xamarin.AndroidX.Activity" Version="1.7.2.1" />
    <PackageReference Include="Xamarin.AndroidX.Activity.Ktx" Version="1.7.2.1" />
  </ItemGroup>
  <ItemGroup>
    <PackageReference Include="Camera.MAUI" Version="1.4.4" />
    <PackageReference Include="CClarke.Plugin.Calendars" Version="1.1.0" />
    <PackageReference Include="CommunityToolkit.Maui" Version="5.2.0" />
    <PackageReference Include="Geolocator.Plugin" Version="1.0.2" />
    <PackageReference Include="Microsoft.AppCenter" Version="5.0.2">
      <TreatAsUsed>true</TreatAsUsed>
    </PackageReference>
    <PackageReference Include="Microsoft.AppCenter.Analytics" Version="5.0.2" />
    <PackageReference Include="Microsoft.AppCenter.Crashes" Version="5.0.2" />
    <PackageReference Include="Microsoft.CSharp" Version="4.7.0" />
    <PackageReference Include="NdefLibrary" Version="4.1.0" />
    <PackageReference Include="Newtonsoft.Json" Version="13.0.3">
      <TreatAsUsed>true</TreatAsUsed>
    </PackageReference>
    <PackageReference Include="Plugin.LocalNotification" Version="10.0.3" />
    <PackageReference Include="Plugin.Maui.Audio" Version="1.0.0" />
    <PackageReference Include="Plugin.NFC" Version="0.1.26" />
    <PackageReference Include="PropertyChanged.Fody" Version="3.4.0" />
    <PackageReference Include="sqlite-net-pcl" Version="1.8.116" />
    <PackageReference Include="ZXing.Net" Version="0.16.9" />
    <PackageReference Include="sqlite-net-pcl" Version="1.8.116" />
    <PackageReference Include="SQLiteNetExtensions.Async" Version="2.1.0" />
    <PackageReference Include="SQLitePCLRaw.core" Version="2.1.0-pre20220207221914">
      <TreatAsUsed>true</TreatAsUsed>
    </PackageReference>
    <PackageReference Include="SQLitePCLRaw.bundle_green" Version="2.1.0-pre20220207221914">
      <TreatAsUsed>true</TreatAsUsed>
    </PackageReference>
    <PackageReference Include="SQLitePCLRaw.provider.dynamic_cdecl" Version="2.1.0-pre20220207221914" />
    <PackageReference Include="SQLitePCLRaw.provider.sqlite3" Version="2.1.0-pre20220207221914">
      <TreatAsUsed>true</TreatAsUsed>
    </PackageReference>
  </ItemGroup>
 ``` 
# MAUI Setup
Some of the setup takes place in your MauiProgram and some takes place in your app.cs.
First off your google-services.json
You stick this in your root folder and set its build action to GoogleServicesJson

Then your MauiProgram
This is the same as how it is in the platforms of crossgeeks
on your builder add the following
```CSharp
private static MauiAppBuilder RegisterFirebaseServices(this MauiAppBuilder builder)
    {
        builder.ConfigureLifecycleEvents(events => {
            #if IOS
            //events.AddiOS(iOS => iOS.WillFinishLaunching((_,options) => {

            //    CrossFirebase.Initialize();
            //    return false;
            //}));
            #elif ANDROID
            var usercategories = new NotificationUserCategory[]
            {
                new NotificationUserCategory("StillAlive", new List<NotificationUserAction>
                {
                    new NotificationUserAction("Yes","Yes", NotificationActionType.Foreground),
                    new NotificationUserAction("No","No", NotificationActionType.Foreground)

                })


            };

            events.AddAndroid(android => android.OnCreate((activity, _) =>            
            FirebasePushNotificationManager.Initialize(usercategories, true, false, true)

            ));
        #endif
        });
  builder.Services.AddSingleton<IPushNotificationHandler, DefaultPushNotificationHandler>();
  return builder;
}
``` 
This initializes firebase notifications.
Now in your app.cs or wherever you want you can add your handler functions.
like
```CSharp
BetterFirebasePushNotification.Current.OnNotificationReceived += async (s, p) =>
{
  //p = your notification
}
```
Or maybe you want to listen for token refreshes and send the new token to a backend
```CSharp
 BetterFirebasePushNotification.Current.OnTokenRefresh += async (s, p) =>
{
      if (p.Token != null && p.Token != "")
      {
                    var request = new RegistrationUpdate()
                    {
                        RegToken = p.Token,
                        SerialKey = Helpers.Settings.License.Replace("/", "").Replace("\\", "").Replace('"', ' ').Trim()
                    };
                    var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
                    await RestManager.Instance.ExecuteRest("SetPushtoken", stringContent);
      }
}
```

# What Now
For now i have to test ios.
Also for some reason the lifecycle OnCreate isnt triggered every time I click on the debug button in visual studio. but it is then triggered if i background and foreground the app. So have some searching to do 🔍
  
