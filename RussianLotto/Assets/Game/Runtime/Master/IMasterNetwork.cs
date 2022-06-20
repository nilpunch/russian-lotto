namespace RussianLotto.Networking
{
    public interface IMasterNetwork : INetwork
    {
        public IMasterRoom MasterRoom { get; }

        public bool IsMasterClient { get; }

        public void DispatchCommands();
    }
}
