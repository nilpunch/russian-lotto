namespace RussianLotto.Input
{
    public interface ISwitch<out T>
    {
        public T State { get; }
    }
}
