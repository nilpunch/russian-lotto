using BananaParty.BehaviorTree;
using Photon.Realtime;

namespace RussianLotto.Application
{
    public class DisconnectFromServerNode : BehaviorNode
    {
        private readonly LoadBalancingClient _photon;

        public DisconnectFromServerNode(LoadBalancingClient photon)
        {
            _photon = photon;
        }

        public override BehaviorNodeStatus OnExecute(long time)
        {
            if (!Started)
            {
                _photon.Disconnect();
            }

            return _photon.IsConnectedAndReady == false ? BehaviorNodeStatus.Success : BehaviorNodeStatus.Running;
        }
    }
}
