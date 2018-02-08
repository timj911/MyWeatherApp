using System;
using Android.App;
using Android.Widget;
using Android.OS;
using Square.Picasso;
using System.Globalization;
using static Android.Widget.ImageView;
using Android.Views.Animations;
using Android.Net;
using CheeseBind;

namespace MyWeatherApp.Droid
{
    [Activity(Label = "Weather")]
    public class MainActivity : Activity
    {

        [BindView(Resource.Id.DateTextView)]
        public TextView dateTextView;

        [BindView(Resource.Id.MaxTempTextView)]
        public TextView maxTempTextView;

        [BindView(Resource.Id.MinTempTextView)]
        public TextView minTempTextView;

        [BindView(Resource.Id.LocationTextView)]
        public TextView locationTextView;

        [BindView(Resource.Id.DescriptionTextView)]
        public TextView descriptionTextView;

        [BindView(Resource.Id.WeatherImageView)]
        public ImageView weatherImageView;

        [BindView(Resource.Id.progressBarHolder)]
        public FrameLayout pbHolder;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Main);

            //Bind view widgets via cheese knife
            Cheeseknife.Bind(this);

            Run();
        }

        public void Run()
        {
            if (CheckInternetConnection())
            {
                GetWeatherInfoAsync();
            }
            else
            {
                HandleNoInternet();
            }
        }

        public bool CheckInternetConnection()
        {
            ConnectivityManager connectivityManager = (ConnectivityManager)GetSystemService(ConnectivityService);
            NetworkInfo networkInfo = connectivityManager.ActiveNetworkInfo;
            return networkInfo != null && networkInfo.IsConnected;
        }

        public void ClearWidgets()
        {
            dateTextView.Text = "";
            maxTempTextView.Text = "";
            minTempTextView.Text = "";
            locationTextView.Text = "";
            descriptionTextView.Text = "";
        }

        public void HandleNoInternet()
        {
            AlertDialog.Builder alertDialog = new AlertDialog.Builder(this);

            alertDialog.SetTitle("Please note");

            alertDialog.SetMessage("There is no internet connection please check your internet settings and retry");

            alertDialog.SetNegativeButton("Retry", (c, ev) =>
            {
                weatherImageView.SetImageResource(Resource.Mipmap.noInternet);
                descriptionTextView.Text = "No internet connection...";
            });

            alertDialog.Show();
        }

        public async void GetWeatherInfoAsync()
        {
            AlphaAnimation inAnimation;
            AlphaAnimation outAnimation;

            //Show loader
            inAnimation = new AlphaAnimation(0f, 1f);
            inAnimation.Duration = 200;
            pbHolder.Animation = inAnimation;
            pbHolder.Visibility = Android.Views.ViewStates.Visible;

            //Get weather
            Weather weatherMan = await Core.GetWeather();

            //Hide loader
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

    }
}
