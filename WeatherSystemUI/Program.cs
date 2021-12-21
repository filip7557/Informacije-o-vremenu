using System;
using System.Collections.Generic;
using System.IO;
using WeatherSystemLogic;

namespace WeatherSystemUI
{
    class Program
    {
        static void Main(string[] args)
        {
            // RunDemoForHW1();
            // RunDemoForHW2();
            // RunDemoForHW3();
            RunDemoForHW4();
        }

        private static void RunDemoForHW1()
        {
            Weather current = new Weather();
            current.SetTemperature(24.12);
            current.SetWindSpeed(3.5);
            current.SetHumidity(0.56);
            Console.WriteLine("Weather info:\n"
                + "\ttemperature: " + current.GetTemperature() + "\n"
                + "\thumidity: " + current.GetHumidity() + "\n"
                + "\twind speed: " + current.GetWindSpeed() + "\n");
            Console.WriteLine("Feels like: " + current.CalculateFeelsLikeTemperature());

            Console.WriteLine("Finding weather info with largest windchill!");
            const int Count = 5;
            double[] temperatures = new double[Count] { 8.33, -1.45, 5.00, 12.37, 7.67 };
            double[] humidities = new double[Count] { 0.3790, 0.4555, 0.743, 0.3750, 0.6612 };
            double[] windSpeeds = new double[Count] { 4.81, 1.5, 5.7, 4.9, 1.2 };

            Weather[] weathers = new Weather[Count];
            for (int i = 0; i < weathers.Length; i++)
            {
                weathers[i] = new Weather(temperatures[i], humidities[i], windSpeeds[i]);
                Console.WriteLine("Windchill for weathers[" + i + "] is: " + weathers[i].CalculateWindChill());
            }
            Weather largestWindchill = ForecastUtilities.FindWeatherWithLargestWindchill(weathers);
            Console.WriteLine(
                "Weather info:" + largestWindchill.GetTemperature() + ", " +
                largestWindchill.GetHumidity() + ", " + largestWindchill.GetWindSpeed()
            );
        }

        private static void RunDemoForHW2()
        {
            DateTime monday = new DateTime(2021, 11, 8);
            Weather mondayWeather = new Weather(6.17, 56.13, 4.9);
            DailyForecast mondayForecast = new DailyForecast(monday, mondayWeather);
            Console.WriteLine(monday.ToString("dd/MM/yyyy HH:mm:ss", null));
            Console.WriteLine(mondayWeather.ToString());
            Console.WriteLine(mondayForecast.ToString());

            // Assume a valid input file (correct format).
            // Assume that the number of rows in the text file is always 7. 
            string fileName = "weather.forecast";
            if (File.Exists(fileName) == false)
            {
                Console.WriteLine("The required file does not exist. Please create it, or change the path.");
                return;
            }

            string[] dailyWeatherInputs = File.ReadAllLines(fileName);
            DailyForecast[] dailyForecasts = new DailyForecast[dailyWeatherInputs.Length];
            for (int i = 0; i < dailyForecasts.Length; i++)
            {
                dailyForecasts[i] = ForecastUtilities.Parse(dailyWeatherInputs[i]);
            }
            WeeklyForecast weeklyForecast = new WeeklyForecast(dailyForecasts);
            Console.WriteLine(weeklyForecast.ToString());
            Console.WriteLine("Maximal weekly temperature:");
            Console.WriteLine(weeklyForecast.GetMaxTemperature());
            Console.WriteLine(weeklyForecast[0].ToString());
        }

        private static void RunDemoForHW3()
        {
            const int weatherCount = 10;
            double minTemperature = -25.00, maxTemperature = 43.00;
            double minHumidity = 0.00, maxHumidity = 100.00;
            double minWindSpeed = 0.00, maxWindSpeed = 10.00;
            Random generator = new Random();

            IRandomGenerator randomGenerator = new UniformGenerator(generator);
            WeatherGenerator weatherGenerator = new WeatherGenerator(
                minTemperature, maxTemperature,
                minHumidity, maxHumidity,
                minWindSpeed, maxWindSpeed,
                randomGenerator
            );

            Weather[] uniformWeathers = new Weather[weatherCount];
            for (int i = 0; i < uniformWeathers.Length; i++)
            {
                uniformWeathers[i] = weatherGenerator.Generate();
            }

            randomGenerator = new BiasedGenerator(generator);
            weatherGenerator.SetGenerator(randomGenerator);
            Weather[] winterWeathers = new Weather[weatherCount];
            for (int i = 0; i < winterWeathers.Length; i++)
            {
                winterWeathers[i] = weatherGenerator.Generate();
            }

            IPrinter[] uniformPrinters = new IPrinter[]
            {
                new ConsolePrinter(ConsoleColor.DarkYellow),
                new FilePrinter(@"uniformWeathers.txt"),
            };
            ForecastUtilities.PrintWeathers(uniformPrinters, uniformWeathers);

            IPrinter[] winterPrinters = new IPrinter[]
            {
                new ConsolePrinter(ConsoleColor.Green),
                new FilePrinter(@"winterWeathers.txt"),
            };
            ForecastUtilities.PrintWeathers(winterPrinters, winterWeathers);
        }

        private static void RunDemoForHW4()
        {
            double minTemperature = -25.00, maxTemperature = 43.00;
            double minHumidity = 0.0, maxHumidity = 100.00;
            double minWindSpeed = 0.00, maxWindSpeed = 10.00;
            IRandomGenerator randomGenerator = new UniformGenerator(new Random());
            WeatherGenerator weatherGenerator = new WeatherGenerator(
                minTemperature, maxTemperature,
                minHumidity, maxHumidity,
                minWindSpeed, maxWindSpeed,
                randomGenerator
            );

            // Creating the repository and adding items.
            DailyForecastRepository repository = new DailyForecastRepository();
            repository.Add(new DailyForecast(DateTime.Now, weatherGenerator.Generate()));
            repository.Add(new DailyForecast(DateTime.Now.AddDays(1), weatherGenerator.Generate()));
            repository.Add(new DailyForecast(DateTime.Now.AddDays(2), weatherGenerator.Generate()));
            Console.WriteLine($"Current state of repository:{Environment.NewLine}{repository}");

            // Adding a new forecast for the same day should replace the old one:
            repository.Add(new DailyForecast(DateTime.Now.AddHours(2), weatherGenerator.Generate()));
            Console.WriteLine($"Current state of repository:{Environment.NewLine}{repository}");

            // Adding multiple forecasts, the ones for existing days should replace the old ones:
            List<DailyForecast> forecasts = new List<DailyForecast>() {
        new DailyForecast(DateTime.Now.AddDays(2), weatherGenerator.Generate()),
        new DailyForecast(DateTime.Now.AddDays(3), weatherGenerator.Generate()),
        new DailyForecast(DateTime.Now.AddDays(4), weatherGenerator.Generate()),
    };
            repository.Add(forecasts);
            Console.WriteLine($"Current state of repository:{Environment.NewLine}{repository}");

            // Removing forecasts based on date:
            try
            {
                repository.Remove(DateTime.Now);
                repository.Remove(DateTime.Now.AddDays(100));
            }
            catch (NoSuchDailyWeatherException exception)
            {
                Console.WriteLine(exception.Message);
            }
            Console.WriteLine($"Current state of repository:{Environment.NewLine}{repository}");

            // Iterating over a custom object:
            Console.WriteLine("Temperatures:");
            foreach (DailyForecast dailyForecast in repository)
            {
                Console.WriteLine($"-> {dailyForecast.Weather.GetTemperature()}");
            }

            // Deep clone:
            DailyForecastRepository copy = new DailyForecastRepository(repository);
            Console.WriteLine($"Original repository:{Environment.NewLine}{repository}");
            Console.WriteLine($"Cloned repository:{Environment.NewLine}{copy}");

            DailyForecast forecastToAdd = new DailyForecast(DateTime.Now, new Weather(-2.0, 47.12, 2.1));
            copy.Add(forecastToAdd);

            Console.WriteLine($"Original repository:{Environment.NewLine}{repository}");
            Console.WriteLine($"Cloned repository:{Environment.NewLine}{copy}");
        }
    }
}
