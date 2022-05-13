using BananaParty.BehaviorTree;
using RussianLotto.Networking;

namespace RussianLotto.Behavior
{
    public class IsConnectedToServer : BehaviorNode
    {
        private readonly INetwork _network;

        public IsConnectedToServer(INetwork network)
        {
            _network = network;
        }

        public override BehaviorNodeStatus OnExecute(long time)
        {
            return _network.IsConnected ? BehaviorNodeStatus.Success : BehaviorNodeStatus.Failure;
        }
    }
}
