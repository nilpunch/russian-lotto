namespace RussianLotto.Client
{
    public interface IFactory<out T>
    {
        T Create();
    }
}
