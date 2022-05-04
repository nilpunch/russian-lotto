namespace RussianLotto.Client
{
    public interface IRandomNumberGenerator
    {
        int Next();
        int Range(int inclusiveMin, int exclusiveMax);
    }
}
