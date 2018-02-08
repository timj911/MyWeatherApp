using Foundation;
using System;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using UIKit;

namespace MyWeatherApp.iOS
{
    public partial class ViewController : UIViewController
    {

        public ViewController(IntPtr handle) : base(handle)
        {

        }

        LoadingOverlay loadPop;

        public override async void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            try
            {
                var bounds = UIScreen.MainScreen.Bounds;

                loadPop = new LoadingOverlay(bounds);
                View.Add(loadPop);

                Weather weatherMan = await Core.GetWeather();

                RegionInfo countryName = new RegionInfo(weatherMan.Country);

                dateLabel.Text = string.Format("Today: {0}", DateTime.Today.ToLongDateString());
                descriptionLabel.Text = weatherMan.Description;
                maxLabel.Text = string.Format("Max: {0} °C", weatherMan.MaxTemp);
                minLabel.Text = string.Format("Min: {0} °C", weatherMan.MinTemp);
                locationLabel.Text = string.Format("{0}, {1}", weatherMan.Location, countryName.DisplayName);

                weatherImage.Image = await LoadImage(string.Format("http://openweathermap.org/img/w/{0}.png", weatherMan.WeatherIcon));

                loadPop.Hide();
            }
            catch (Exception e)
            {
                loadPop.Hide();

                switch (e.Message)
                {

                    case "A geolocation error occured: Unauthorized":
                        descriptionLabel.Text = "Please set location on emulator";
                        break;

                    case "A task was canceled.":
                        var okAlertController = UIAlertController.Create("Location error", "Please set location on emulator", UIAlertControllerStyle.Alert);
                        okAlertController.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Default, null));
                        PresentViewController(okAlertController, true, null);
                        break;
                }
            }
        }

        public async Task<UIImage> LoadImage(string imageUrl)
        {
            var httpClient = new HttpClient();

            Task<byte[]> contentsTask = httpClient.GetByteArrayAsync(imageUrl);

            var contents = await contentsTask;

            return UIImage.LoadFromData(NSData.FromArray(contents));
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
        }
    }
}
