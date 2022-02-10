namespace Twidlle.Library.Utility
{
    public abstract class RandomGenerator : IRandomGenerator
    {
        private readonly Random _random;

        protected RandomGenerator(int seed) =>
            _random = new Random(seed);

        public int Next(int maxValue) =>
            _random.Next(maxValue);

        public int Next(int minValue, int maxValue) =>
            _random.Next(minValue, maxValue);
    }
}
