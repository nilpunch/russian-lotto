namespace RussianLotto.Networking
{
    public interface IClientCommand
    {
        public void Execute(IClientContext clientContext);
    }
}