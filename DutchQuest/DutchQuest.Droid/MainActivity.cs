using System;
using Acr.UserDialogs;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using HockeyApp.Android;
using Xamarin.Forms;

namespace DutchQuest.Droid
{
    [Activity(Label = "DutchQuest", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            UserDialogs.Init(() => (Activity)Forms.Context);
            CrashManager.Register(this, "ab49e2ccf6c143d38f2be076fe6ad1a3");

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);
            
            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
        }
    }
}



