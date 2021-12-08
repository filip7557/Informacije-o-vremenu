using System;

namespace WeatherSystemLogic
{
    public class ConsolePrinter : IPrinter
    {
        private ConsoleColor consoleColor;

        public ConsolePrinter(ConsoleColor consoleColor)
        {
            this.consoleColor = consoleColor;
        }

        public void PrintWeather(Weather weather)
        {
            Console.ForegroundColor = consoleColor;
            Console.WriteLine(weather.ToString());
            Console.ResetColor();
        }
    }
}
