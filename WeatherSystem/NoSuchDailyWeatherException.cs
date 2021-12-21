using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherSystemLogic
{
    public class NoSuchDailyWeatherException : Exception
    {
        DateTime date;

        public NoSuchDailyWeatherException(DateTime date)
        {
            this.date = date.Date;
        }

        public DateTime Date { get { return date; } }
        public new string Message { get { return $"No daily forecast for {date}"; } }
    }
}
