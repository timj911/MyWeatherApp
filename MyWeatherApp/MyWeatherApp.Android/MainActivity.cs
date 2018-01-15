using System;
using Android.App;
using Android.Widget;
using Android.OS;
using Square.Picasso;
using System.Globalization;
using static Android.Widget.ImageView;

namespace MyWeatherApp.Droid
{
    [Activity(Label = "Weather")]
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

            ProgressBar pb = FindViewById<ProgressBar>(Resource.Id.progressBar1);

            //Get weather
            pb.Visibility = Android.Views.ViewStates.Visible;
            Weather WeerMan = await Core.GetWeather();
            pb.Visibility = Android.Views.ViewStates.Gone;

            //Get Country from Region Info
            RegionInfo countryName = new RegionInfo(WeerMan.Country);


            //Set values    
            dateTextView.Text += DateTime.Today.ToShortDateString();
            maxTempTextView.Text += WeerMan.MaxTemp + " °C";
            minTempTextView.Text += WeerMan.MinTemp + " °C";
            locationTextView.Text += String.Format("{0}, {1}" ,WeerMan.Location, countryName);

            //load waether image
            Picasso.With(this)
            .Load(String.Format("http://openweathermap.org/img/w/{0}.png" ,WeerMan.WeatherIcon))
            .Into(weatherImageView);

            weatherImageView.SetScaleType(ScaleType.FitCenter);

        }

    }
}

