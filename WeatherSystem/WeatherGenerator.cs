using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherSystemLogic
{
    public class WeatherGenerator
    {
        private double minTemp;
        private double maxTemp;
        private double minHumidity;
        private double maxHumidity;
        private double minWindSpeed;
        private double maxWindSpeed;
        private IRandomGenerator random;

        public WeatherGenerator(double minTemp, double maxTemp, double minHumidity, double maxHumidity, double minWindSpeed, double maxWindSpeed, IRandomGenerator random)
        {
            this.minTemp = minTemp;
            this.maxTemp = maxTemp;
            this.minHumidity = minHumidity;
            this.maxHumidity = maxHumidity;
            this.minWindSpeed = minWindSpeed;
            this.maxWindSpeed = maxWindSpeed;
            this.random = random;
        }

        public double MinTemp { get => minTemp; set => minTemp = value; }
        public double MaxTemp { get => maxTemp; set => maxTemp = value; }
        public double MinHumidity { get => minHumidity; set => minHumidity = value; }
        public double MaxHumidity { get => maxHumidity; set => maxHumidity = value; }
        public double MinWindSpeed { get => minWindSpeed; set => minWindSpeed = value; }
        public double MaxWindSpeed { get => maxWindSpeed; set => maxWindSpeed = value; }

        public void SetGenerator(IRandomGenerator random)
        {
            this.random = random;
        }

        public Weather Generate()
        {
            double temp = random.GenerateRandomValue(MinTemp, MaxTemp);
            double humidity = random.GenerateRandomValue(MinHumidity, MaxHumidity);
            double windSpeed = random.GenerateRandomValue(MinWindSpeed, MaxWindSpeed);

            return new Weather(temp, humidity, windSpeed);
        }
    }
}
