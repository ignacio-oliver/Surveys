using Android.App;
using Android.OS;
using Com.OneSignal;
using Xamarin.Forms.Platform.Android;

namespace Surveys.Droid
{
    [Activity(MainLauncher = true)]
    public class MainActivity : FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            OneSignal.Current.StartInit("7278293c-d2bd-4adb-93cc-55db3c10ee48").EndInit();
            OneSignal.Current.SetEmail("ignacio.oliver@gmail.com");

            Xamarin.Forms.Forms.Init(this, bundle);

            LoadApplication(new Core.App());

        }
    }
}

