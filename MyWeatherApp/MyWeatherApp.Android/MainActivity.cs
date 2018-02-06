using System;
using Android.App;
using Android.Widget;
using Android.OS;
using Square.Picasso;
using System.Globalization;
using static Android.Widget.ImageView;
using Android.Views.Animations;
using Android.Net;

namespace MyWeatherApp.Droid
{
    [Activity(Label = "Weather")]
    public class MainActivity : Activity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.main);

            bool isOnline = CheckInternetConnection();

            if (isOnline)
            {
                GetWeatherInfo();
            }
            else
            {
                NoInternet();
            }

        }

        public bool CheckInternetConnection()
        {
            ConnectivityManager connectivityManager = (ConnectivityManager)GetSystemService(ConnectivityService);
            NetworkInfo networkInfo = connectivityManager.ActiveNetworkInfo;
            return networkInfo != null && networkInfo.IsConnected;
        }

        public async void GetWeatherInfo()
        {
            AlphaAnimation inAnimation;
            AlphaAnimation outAnimation;

            //Get the widgets
            TextView dateTextView = FindViewById<TextView>(Resource.Id.DateTextView);
            TextView maxTempTextView = FindViewById<TextView>(Resource.Id.MaxTempTextView);
            TextView minTempTextView = FindViewById<TextView>(Resource.Id.MinTempTextView);
            TextView locationTextView = FindViewById<TextView>(Resource.Id.LocationTextView);
            TextView descriptionTextView = FindViewById<TextView>(Resource.Id.textView2);
            ImageView weatherImageView = FindViewById<ImageView>(Resource.Id.WeatherImageView);
            FrameLayout pbHolder = FindViewById<FrameLayout>(Resource.Id.progressBarHolder);

            //Get weather
            inAnimation = new AlphaAnimation(0f, 1f);
            inAnimation.Duration = 200;
            pbHolder.Animation = inAnimation;
            pbHolder.Visibility = Android.Views.ViewStates.Visible;

            Weather weatherMan = await Core.GetWeather();

            outAnimation = new AlphaAnimation(1f, 0f);
            outAnimation.Duration = 200;
            pbHolder.Animation = outAnimation;
            pbHolder.Visibility = Android.Views.ViewStates.Gone;

            //Get country name from RegionInfo
            RegionInfo countryName = new RegionInfo(weatherMan.Country);

            //Set values    
            dateTextView.Text = string.Format("Today: {0}", DateTime.Today.ToLongDateString());
            maxTempTextView.Text = string.Format("Max: {0} °C", weatherMan.MaxTemp);
            minTempTextView.Text = string.Format("Min: {0} °C", weatherMan.MinTemp);
            locationTextView.Text = string.Format("{0}, {1}", weatherMan.Location, countryName.DisplayName);
            descriptionTextView.Text = weatherMan.Description;

            //load weather image
            Picasso.With(this)
            .Load(string.Format("http://openweathermap.org/img/w/{0}.png", weatherMan.WeatherIcon))
            .Into(weatherImageView);

            weatherImageView.SetScaleType(ScaleType.FitCenter);
        }

        public void NoInternet()
        {
            Console.WriteLine("Touch!!!");
        }

    }
}
