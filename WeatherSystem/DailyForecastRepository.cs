using System;
using System.Collections;
using System.Collections.Generic;

namespace WeatherSystemLogic
{
    public class DailyForecastRepository : IEnumerable
    {
        List<DailyForecast> dailyForecasts;

        public DailyForecastRepository()
        {
            dailyForecasts = new List<DailyForecast>();
        }

        public DailyForecastRepository(DailyForecastRepository original)
        {
            dailyForecasts = new List<DailyForecast>();
            dailyForecasts.AddRange(original.dailyForecasts);
        }

        public void Add(DailyForecast dailyForecast)
        {
            int index = 0;
            bool replacedOldOne = false;
            foreach(var forecast in dailyForecasts)
            {
                if(dailyForecast.Date.Date == forecast.Date.Date)
                {
                    index = dailyForecasts.IndexOf(forecast);
                    replacedOldOne = true;
                }
            }
            if (!replacedOldOne)
                dailyForecasts.Add(dailyForecast);
            else dailyForecasts[index] = dailyForecast;
            dailyForecasts.Sort(new DateComparer());
        }

        public void Add(List<DailyForecast> forecasts)
        {
            List<int> indexes = new List<int>();
            foreach (var dailyForecast in forecasts)
            {
                foreach (var forecast in dailyForecasts)
                {
                    if (dailyForecast.Date.Date == forecast.Date.Date)
                    {
                        int index = dailyForecasts.IndexOf(forecast);
                        dailyForecasts[index].Weather = dailyForecast.Weather;
                        indexes.Add(index);
                    }
                }
            }
            foreach(int index in indexes)
            {
                forecasts.RemoveAt(index);
            }
            dailyForecasts.AddRange(forecasts);
            dailyForecasts.Sort(new DateComparer());
        }

        public IEnumerator GetEnumerator()
        {
            return ((IEnumerable)dailyForecasts).GetEnumerator();
        }

        public void Remove(DateTime date)
        {
            foreach (var forecast in dailyForecasts)
            {
                if (forecast.Date.Date == date.Date)
                {
                    dailyForecasts.Remove(forecast);
                    return;
                }
            }
            throw new NoSuchDailyWeatherException(date);
        }

        public override string ToString()
        {
            string message = "";
            foreach(var dailyForecast in dailyForecasts)
            {
                message += dailyForecast + Environment.NewLine;
            }
            return message;
        }
    }
}
