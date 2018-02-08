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

        public async Task<UIImage> LoadImage(string imageUrl)
        {
            var httpClient = new HttpClient();

            Task<byte[]> contentsTask = httpClient.GetByteArrayAsync(imageUrl);

            // await! control returns to the caller and the task continues to run on another thread
            var contents = await contentsTask;

            // load from bytes
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
