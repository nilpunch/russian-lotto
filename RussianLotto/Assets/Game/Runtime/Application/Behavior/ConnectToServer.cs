using BananaParty.BehaviorTree;
using Photon.Realtime;
using RussianLotto.Networking;

namespace RussianLotto.Application
{
    public class ConnectToServer : BehaviorNode
    {
        private readonly ISocket _socket;

        public ConnectToServer(ISocket socket)
        {
            _socket = socket;
        }

        public override BehaviorNodeStatus OnExecute(long time)
        {
            if (!Started)
            {
                _socket.Connect();
            }

            return _socket.IsConnected ? BehaviorNodeStatus.Success : BehaviorNodeStatus.Running;
        }
    }
}
