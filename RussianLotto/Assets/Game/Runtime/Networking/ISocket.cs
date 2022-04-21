using System;

namespace RussianLotto.Networking
{
    public interface ISocket : IDisposable
    {
        bool IsConnected { get; }
        bool HasUnreadPayloadQueue { get; }
        byte[] ReadPayloadQueue();

        void Connect();
        void Send(byte[] payloadBytes);
        void Disconnect();
    }
}
