using System;

namespace WeatherSystemLogic
{
    public class UniformGenerator : IRandomGenerator
    {
        Random random;

        public UniformGenerator(Random random)
        {
            this.random = random;
        }

        public double GenerateRandomValue(double min, double max)
        {
            return random.NextDouble() * (max - min) + min;
        }
    }
}
