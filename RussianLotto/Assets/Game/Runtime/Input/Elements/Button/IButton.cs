namespace RussianLotto.Input
{
    public interface IButton
    {
        public bool Pressed { get; }
        public void Release();
    }
}
