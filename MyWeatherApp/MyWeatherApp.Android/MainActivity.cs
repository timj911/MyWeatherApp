using Android.App;
using Android.Widget;
using Android.OS;
using System;

namespace MyWeatherApp.Droid
{
    [Activity(Label = "Weather", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Main);


            var button = FindViewById<Button>(Resource.Id.button1);

            button.Click += async (s,e) => {

                Weather weerman = await Core.GetWeather();
                var Temp = FindViewById<TextView>(Resource.Id.PlaceTextView);
                Temp.Text = weerman.Location;
            };

        }

    }
}

