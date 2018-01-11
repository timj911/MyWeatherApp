using Android.App;
using Android.Widget;
using Android.OS;

namespace MyWeatherApp.Droid
{
    [Activity(Label = "Weather", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Main);

        }
    }
}

