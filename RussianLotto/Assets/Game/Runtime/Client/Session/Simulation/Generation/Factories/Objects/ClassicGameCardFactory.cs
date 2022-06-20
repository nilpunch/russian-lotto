namespace RussianLotto.Client
{
    public class ClassicGameCardFactory : CardFactory
    {
        public ClassicGameCardFactory(IRandomNumberGenerator randomNumberGenerator, bool shuffled) : base(randomNumberGenerator, shuffled, 90)
        {
        }
    }
}
