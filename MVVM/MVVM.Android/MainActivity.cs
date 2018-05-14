
using Android.App;
using Android.Content.PM;
using Android.OS;
using Gcm.Client;

namespace MVVM.Droid
{
    [Activity(Label = "Sınıf Anons Mobil", Icon = "@drawable/satX", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public static MainActivity instance;
        protected override void OnCreate(Bundle bundle)
        {
            instance = this;

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            try
            {
                GcmClient.Register(this, App.SenderID);
                GcmClient.CheckDevice(this);
                GcmClient.CheckManifest(this);
            }
            catch { }
            LoadApplication(new App());
        }
    }
}

