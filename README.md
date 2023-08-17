# BetterFireBasePlugin
Attempting to upgrade CrossFireBasePlugin to MAUI

Been using https://github.com/CrossGeeks/FirebasePushNotificationPlugin for my Xamarin app.
this doesnt work for MAUI .net7 so im trying to upgrade it to maui .net7 
but im getting stuck. if i add this to a project by way of custom nuget package the project fails to build.

Either with
Type com.google.android.gms.auth.api.signin.GoogleSignInAccount is defined multiple times
or 
Type com.google.android.gms.auth.api.signin.GoogleSignInOptions$Builder is defined multiple times
