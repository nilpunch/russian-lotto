namespace RussianLotto.Networking
{
    /// <summary>
    /// Network abstract factory.
    /// </summary>
    public interface INetwork
    {
        public ISocket Socket { get; }
        public IRoom Room { get; }
    }
}
