namespace RussianLotto.Networking
{
    /// <summary>
    /// Relay photon abstract factory. Only for Master
    /// </summary>
    public interface IMasterNetwork : INetwork
    {
        public IMaster Master { get; }
    }
}
