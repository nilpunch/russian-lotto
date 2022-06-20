namespace RussianLotto.Client
{
    public class FastGameCardFactory : CardFactory
    {
        public FastGameCardFactory(IRandomNumberGenerator randomNumberGenerator, bool shuffled) : base(randomNumberGenerator, shuffled, 36)
        {
        }
    }
}
