using System;

namespace RussianLotto.Networking
{
    public interface IRoomNetwork : IDisposable
    {
        public bool IsConnectedToRoom { get; }
        public bool HasUnreadCommands { get; }

        public void ConnectToRandomRoom();
        public void ConnectToRoom(string roomName);
        public void Disconnect();

        public IClientCommand ReadCommand();
    }
}
