using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherSystemLogic
{
    public class BiasedGenerator : IRandomGenerator
    {
        Random random;

        public BiasedGenerator(Random random)
        {
            this.random = random;
        }

        public double GenerateRandomValue(double min, double max)
        {
            double halfValue = max / 2;
            double value = 0.0;

            int i = random.Next(1, 7);
            if (i <= 2)
                value = random.NextDouble() * (max - halfValue) + halfValue;
            else if (i > 2 && i <= 6)
                value = random.NextDouble() * (halfValue - min) + min;

            return value;
        }
    }
}
