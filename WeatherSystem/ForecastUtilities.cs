using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace WeatherSystemLogic
{
    public static class ForecastUtilities
    {
        public static DailyForecast Parse(string v)
        {
            string[] parts = v.Split(',');
            Weather weather = new Weather(Convert.ToDouble(parts[1]), Convert.ToDouble(parts[3]), Convert.ToDouble(parts[2]));
            return new DailyForecast(DateTime.ParseExact(parts[0], "dd/MM/yyyy hh:mm:ss", null), weather);
        }

        public static Weather FindWeatherWithLargestWindchill(Weather[] weathers)
        {
            Weather largestWindchill = weathers[0];
            double windchill = weathers[0].CalculateWindChill();
            foreach (Weather weather in weathers)
            {
                double currentWindchill = weather.CalculateWindChill();
                if (currentWindchill > windchill)
                {
                    windchill = currentWindchill;
                    largestWindchill = weather;
                }
            }
            return largestWindchill;
        }
    }
}
