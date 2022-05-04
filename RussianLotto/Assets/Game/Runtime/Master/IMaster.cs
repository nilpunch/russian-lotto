namespace RussianLotto.Networking
{
    public interface IMaster
    {
        public bool IsMasterClient { get; }

        public void DispatchCommands();
    }
}
