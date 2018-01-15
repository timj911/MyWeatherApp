﻿using Plugin.Geolocator;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Threading.Tasks;

namespace MyWeatherApp
{
    public class Core
    {

        public static async Task<Weather> GetWeather()
        {

            try
            {
                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 50;

                TimeSpan ts = TimeSpan.FromMilliseconds(10000);

                var position = await locator.GetPositionAsync(timeout: ts);

                string key = "a080d3b91e8e841d6f6c1ac46fba8e44";
                string queryString = "http://api.openweathermap.org/data/2.5/weather?lat=" + position.Latitude + "&lon=" + position.Longitude + "&appid=" + key + "&units=metric";

                dynamic results = await DataService.getDataFromService(queryString).ConfigureAwait(false);

                if (results["weather"] != null)
                {
                    Weather weather = new Weather
                    {
                        Location = (string)results["name"],
                        MinTemp = (string)results["main"]["temp_min"],
                        MaxTemp = (string)results["main"]["temp_max"],
                        WeatherIcon = (string)results["weather"][0]["icon"]
                    };

                    return weather;
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex);
            }

            return null;

        }

    }
}