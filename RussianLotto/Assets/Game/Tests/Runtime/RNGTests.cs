using System.Linq;
using NUnit.Framework;
using RussianLotto.Client;

namespace RussianLotto.Tests
{
    public class RNGTests
    {
        private const int Iterations = 1000;

        [Test]
        public void ShouldReturnsValueInRange()
        {
            int minValue = 0;
            int maxValue = 3;

            int[] generated = new int[Iterations];
            RandomNumberGenerator rng = new(0);

            for (int i = 0; i < Iterations; ++i)
            {
                generated[i] = rng.Range(minValue, maxValue);
            }

            Assert.True(generated.All(value => value >= minValue && value < maxValue));
        }

    }
}
