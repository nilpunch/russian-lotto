namespace RussianLotto.Client
{
    public class FastCardFactory : CardFactory
    {
        public FastCardFactory(IRandomNumberGenerator randomNumberGenerator, bool shuffled) : base(randomNumberGenerator, shuffled, 36)
        {
        }
    }
}
