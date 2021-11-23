using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherSystemLogic
{
    public class WeeklyForecast
    {
        private DailyForecast[] dailyForecasts;

        public WeeklyForecast(DailyForecast[] dailyForecasts)
        {
            this.dailyForecasts = dailyForecasts;
        }

        public DailyForecast this[int index]
        {
            get { return dailyForecasts[index]; }
        }

        public string GetAsString()
        {
            string message = "";
            foreach (var dailyForecast in dailyForecasts)
            {
                message += dailyForecast.GetAsString() + '\n';
            }
            return message;
        }

        public double GetMaxTemperature()
        {
            DailyForecast maxTemperature = dailyForecasts[0];
            foreach (var dailyForecast in dailyForecasts)
            {
                if (dailyForecast > maxTemperature)
                    maxTemperature = dailyForecast;
            }
            return maxTemperature.Temperature;
        }

    }
}
