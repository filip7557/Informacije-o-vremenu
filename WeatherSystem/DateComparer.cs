using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace WeatherSystemLogic
{
    class DateComparer : IComparer<DailyForecast>
    {
        public int Compare([AllowNull] DailyForecast x, [AllowNull] DailyForecast y)
        {
            if (x.Date.Date > y.Date.Date) return 1;
            else if (x.Date.Date == y.Date.Date) return 0;
            else return -1;
        }
    }
}
