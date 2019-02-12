using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using SnowConeTycoon.Shared;

namespace SnowConeTycoon.Android
{
    [Activity(Label = "Snow Cones"
        , MainLauncher = true
        , Icon = "@mipmap/ic_launcher"
        , RoundIcon = "@mipmap/ic_launcher_round"
        , Theme = "@style/Theme.Splash"
        , AlwaysRetainTaskState = true
        //, LaunchMode = Android.Content.PM.LaunchMode.SingleInstance
        , ScreenOrientation = ScreenOrientation.Portrait
        , ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.Keyboard | ConfigChanges.KeyboardHidden | ConfigChanges.ScreenSize | ConfigChanges.ScreenLayout)]
    public class Activity1 : Microsoft.Xna.Framework.AndroidGameActivity
    {
        private const string ListenConnectionString = "Endpoint=sb://chrosgames.servicebus.windows.net/;SharedAccessKeyName=SnowConeTycoonDefaultPolicy;SharedAccessKey=QOOEZ1UbIsiR6kLlZYlN3DsRXdYMBAvvyHGEfDjK1xs=";
        private const string NotificationHubName = "SnowConeTycoon";

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            var g = new AndroidWrapperGame(this);
            SetContentView((View)g.Services.GetService(typeof(View)));
            g.Run();
        }
    }
}

