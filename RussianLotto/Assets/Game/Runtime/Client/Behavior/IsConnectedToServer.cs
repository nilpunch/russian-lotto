using BananaParty.BehaviorTree;
using RussianLotto.Networking;

namespace RussianLotto.Behavior
{
    public class IsConnectedToServer : BehaviorNode
    {
        private readonly ISocket _socket;

        public IsConnectedToServer(ISocket socket)
        {
            _socket = socket;
        }

        public override BehaviorNodeStatus OnExecute(long time)
        {
            return _socket.IsConnected ? BehaviorNodeStatus.Success : BehaviorNodeStatus.Failure;
        }
    }
}
