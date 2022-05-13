using BananaParty.BehaviorTree;
using RussianLotto.Networking;

namespace RussianLotto.Behavior
{
    public class ConnectToServer : BehaviorNode
    {
        private readonly INetwork _network;

        public ConnectToServer(INetwork network)
        {
            _network = network;
        }

        public override BehaviorNodeStatus OnExecute(long time)
        {
            if (!Started)
            {
                _network.Connect();
            }

            return _network.IsConnected ? BehaviorNodeStatus.Success : BehaviorNodeStatus.Running;
        }
    }
}
