using System;

namespace RussianLotto.Networking
{
    /// <summary>
    /// Network abstract factory.
    /// </summary>
    public interface INetwork : IDisposable
    {
        public IRoom Room { get; }

        public bool IsConnected { get; }

        public void Connect();
        public void Disconnect();
    }
}
