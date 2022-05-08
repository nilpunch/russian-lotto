namespace RussianLotto.Client
{
    public class NumbersFactory : IFactory<INumbers>
    {
        private readonly int _numbersAmount;
        private readonly IRandomNumberGenerator _randomNumberGenerator;

        public NumbersFactory(int numbersAmount, IRandomNumberGenerator randomNumberGenerator)
        {
            _numbersAmount = numbersAmount;
            _randomNumberGenerator = randomNumberGenerator;
        }

        public INumbers Create()
        {
            int[] numbers = new int[_numbersAmount];

            for (int i = 0; i < _numbersAmount; i++)
                numbers[i] = i + 1;

            for (int i = 1; i < _numbersAmount; i++)
            {
                numbers.Swap(i - 1, _randomNumberGenerator.Range(i, _numbersAmount));
            }

            return new Numbers(numbers);
        }
    }
}
