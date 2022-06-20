namespace RussianLotto.Client
{
    public interface IFactory<out TType>
    {
        TType Create();
    }
}
