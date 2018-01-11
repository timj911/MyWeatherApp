using System;
using System.Threading.Tasks;

namespace MyWeatherApp
{
    class Core
    {

        public static async Task<Weather> GetWeather(string lat, string lon)
        {

            string key = "a080d3b91e8e841d6f6c1ac46fba8e44";
            string queryString = "http://api.openweathermap.org/data/2.5/weather?lat="+ lat +"&lon=" + lon + "&appid=" + key + "&units=metric";

            dynamic results = await DataService.getDataFromService(queryString).ConfigureAwait(false);

            if (results["weather"] != null)
            {
                Weather weather = new Weather();
                weather.Location = (string)results["name"];
                weather.MinTemp = (string)results["main"]["temp_min"];
                weather.MaxTemp = (string)results["main"]["temp_max"];
                weather.WeatherIcon = (string)results["weather"][0]["icon"];

                return weather;
            }
            else
            {
                return null;
            }
        }

    }
}
