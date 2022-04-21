using System;

namespace RussianLotto.Networking
{
    public class RoomNetwork : IRoomNetwork
    {
        private readonly ISocket _socket;

        public RoomNetwork(ISocket socket)
        {
            _socket = socket;
        }
        
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public bool IsConnectedToRoom { get; }
        public bool HasUnreadCommands { get; }
        public void ConnectToRandomRoom()
        {
            throw new NotImplementedException();
        }

        public void ConnectToRoom(string roomName)
        {
            throw new NotImplementedException();
        }

        public void Disconnect()
        {
            throw new NotImplementedException();
        }

        public IClientCommand ReadCommand()
        {
            throw new NotImplementedException();
        }
    }
}
