To use this app.
Go into the project file and change the ApplicationId
Log into your apple developer enviornment and create all the certificates for an ios app.
Then create an anps certificate in the apple developer enviornment. Download this and remember the key id and the team id.
Then go into google firebase console. And either create a project or add apps to your existing project.
Create an android app and ios app in the console.
Download the googleService-info.plist and google-services.json.
the plist is for ios and the .json is for android.

Make sure to include this in your project file
 <BundleResource Include="GoogleService-Info.plist" />   
<GoogleServicesJson Include="google-services.json" />
You can add an itemgroup around these with a targetframework condition. but I noticed that android doesnt mind if it also has the plist and vice versa.

You can have these files in their respective platforms But then make sure to add a link to their references in the project file
Like this.
<ItemGroup Condition="$(TargetFramework.Contains('-android'))">
	<GoogleServicesJson Include="Platforms\Android\Resources\google-services.json" Link="Platforms\Android\Resources\google-services.json" />
</ItemGroup>

<ItemGroup Condition="$(TargetFramework.Contains('-ios'))">
	<BundleResource Include="Platforms\iOS\GoogleService-Info.plist" Link="GoogleService-Info.plist" />
</ItemGroup>

Now if you are debugging for ios then leave the value of aps-environment to development in the entitlements.plist file.
For release please change this to production. I believe you can also exclude the file if you dont need any other entitlements like NFC.




