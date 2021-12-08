using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherSystemLogic
{
    public class Weather
    {
        private double currentTemperature; //°C
        private double relativeHumidity; //%
        private double windSpeed; //km/h

        public Weather()
        {
            currentTemperature = 0;
            relativeHumidity = 0;
            windSpeed = 0;
        }

        public Weather(double currentTemperature, double relativeHumidity, double windSpeed)
        {
            this.currentTemperature = currentTemperature;
            this.relativeHumidity = relativeHumidity;
            this.windSpeed = windSpeed;
        }

        public void SetTemperature(double currentTemperature)
        {
            this.currentTemperature = currentTemperature;
        }

        public double GetTemperature()
        {
            return this.currentTemperature;
        }

        public void SetHumidity(double relativeHumidity)
        {
            this.relativeHumidity = relativeHumidity;
        }

        public double GetHumidity()
        {
            return this.relativeHumidity;
        }

        public void SetWindSpeed(double windSpeed)
        {
            this.windSpeed = windSpeed;
        }

        public double GetWindSpeed()
        {
            return this.windSpeed;
        }

        public double CalculateFeelsLikeTemperature()
        {
            double feelsLike = -8.78469475556 +
                                1.61139411 * this.currentTemperature +
                                2.33854883889 * this.relativeHumidity +
                                -0.14611605 * this.currentTemperature * this.relativeHumidity +
                                -0.012308094 * Math.Pow(this.currentTemperature, 2) +
                                -0.0164248277778 * Math.Pow(this.relativeHumidity, 2) +
                                0.002211732 * Math.Pow(this.currentTemperature, 2) * this.relativeHumidity +
                                0.00072546 * this.currentTemperature * Math.Pow(this.relativeHumidity, 2) +
                                -0.000003582 * Math.Pow(this.currentTemperature, 2) * Math.Pow(this.relativeHumidity, 2);
            return feelsLike;
        }

        public double CalculateWindChill()
        {
            if (this.currentTemperature > 10 || this.windSpeed < 4.8)
                return 0;
            return 13.12 + 0.6215 * this.currentTemperature - 11.37 * Math.Pow(this.windSpeed, 0.16) + 0.3965 * Math.Pow(this.windSpeed, 0.16);
        }

        public override string ToString()
        {
            return $"T={GetTemperature()}°C, w={GetWindSpeed()}km/h, h={GetHumidity()}%";
        }
    }
}
