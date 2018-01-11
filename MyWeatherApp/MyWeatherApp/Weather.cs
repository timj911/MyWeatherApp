namespace MyWeatherApp
{
    public class Weather
    {
        public string Location { get; set; }
        public string MinTemp { get; set; }
        public string MaxTemp { get; set; }
        public string WeatherIcon { get; set; }


        public Weather()
        {  
            this.Location = " ";
            this.MinTemp = " ";
            this.MaxTemp = " ";
            this.WeatherIcon = " ";
        }
    }
}
