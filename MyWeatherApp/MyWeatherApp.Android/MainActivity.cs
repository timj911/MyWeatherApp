using Android.App;
using Android.Widget;
using Android.OS;
using System;
using Square.Picasso;

namespace MyWeatherApp.Droid
{
    [Activity(Label = "Weather", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Main);

            //Get the Widgets
            TextView dateTextView = FindViewById<TextView>(Resource.Id.DateTextView);
            TextView maxTempTextView = FindViewById<TextView>(Resource.Id.MaxTempTextView);
            TextView minTempTextView = FindViewById<TextView>(Resource.Id.MinTempTextView);
            TextView locationTextView = FindViewById<TextView>(Resource.Id.LocationTextView);
            ImageView weatherImageView = FindViewById<ImageView>(Resource.Id.WeatherImageView);

            //Get weather
             Weather WeerMan = await Core.GetWeather();

            //Set values    
            dateTextView.Text += DateTime.Today;
            maxTempTextView.Text += WeerMan.MaxTemp;
            minTempTextView.Text += WeerMan.MinTemp;
            locationTextView.Text += WeerMan.Location;

            //load waether image
            Picasso.With(this)
            .Load(WeerMan.WeatherIcon)
            .Into(weatherImageView);

        }

    }
}

