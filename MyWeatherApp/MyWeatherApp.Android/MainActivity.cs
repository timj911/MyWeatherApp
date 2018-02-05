using System;
using Android.App;
using Android.Widget;
using Android.OS;
using Square.Picasso;
using System.Globalization;
using static Android.Widget.ImageView;
using Android.Views.Animations;

namespace MyWeatherApp.Droid
{
    [Activity(Label = "Weather")]
    public class MainActivity : Activity
    {

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Main);

            AlphaAnimation inAnimation;
            AlphaAnimation outAnimation;

            //Get the widgets
            TextView dateTextView = FindViewById<TextView>(Resource.Id.DateTextView);
            TextView maxTempTextView = FindViewById<TextView>(Resource.Id.MaxTempTextView);
            TextView minTempTextView = FindViewById<TextView>(Resource.Id.MinTempTextView);
            TextView locationTextView = FindViewById<TextView>(Resource.Id.LocationTextView);
            ImageView weatherImageView = FindViewById<ImageView>(Resource.Id.WeatherImageView);
            FrameLayout pbHolder = FindViewById<FrameLayout>(Resource.Id.progressBarHolder);

            //Get weather
            inAnimation = new AlphaAnimation(0f, 1f);
            inAnimation.Duration = 200;
            pbHolder.Animation = inAnimation;
            pbHolder.Visibility = Android.Views.ViewStates.Visible;

            Weather WeerMan = await Core.GetWeather();

            outAnimation = new AlphaAnimation(1f, 0f);
            outAnimation.Duration = 200;
            pbHolder.Animation = outAnimation;
            pbHolder.Visibility = Android.Views.ViewStates.Gone;

            //Get country name from RegionInfo
            RegionInfo countryName = new RegionInfo(WeerMan.Country);

            //Set values    
            dateTextView.Text = string.Format("Today: {0}" + DateTime.Today.ToLongDateString());
            maxTempTextView.Text = string.Format("Max: {0} °C", WeerMan.MaxTemp);
            minTempTextView.Text = string.Format("Min: {0} °C", WeerMan.MinTemp);
            locationTextView.Text = string.Format("{0}, {1}" ,WeerMan.Location, countryName.DisplayName);

            //load weather image
            Picasso.With(this)
            .Load(string.Format("http://openweathermap.org/img/w/{0}.png" ,WeerMan.WeatherIcon))
            .Into(weatherImageView);

            weatherImageView.SetScaleType(ScaleType.FitCenter);

        }

    }
}
