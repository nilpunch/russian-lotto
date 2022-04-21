using System;

namespace RussianLotto.Client
{
    public class RandomNumberGenerator : IRandomNumberGenerator
    {
        private readonly Random _random;

        public RandomNumberGenerator(int seed)
        {
            _random = new Random(seed);
        }

        public int Next()
        {
            return _random.Next();
        }

        public int Range(int inclusiveMin, int exclusiveMax)
        {
            if (inclusiveMin > exclusiveMax)
                throw new ArgumentOutOfRangeException();

            if (inclusiveMin == exclusiveMax || inclusiveMin == exclusiveMax - 1)
                return inclusiveMin;

            int delta = _random.Next() % ((exclusiveMax - 1) - inclusiveMin);
            return inclusiveMin + delta;
        }
    }
}
