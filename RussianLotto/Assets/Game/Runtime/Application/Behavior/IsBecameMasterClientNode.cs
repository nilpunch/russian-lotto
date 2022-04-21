using BananaParty.BehaviorTree;
using Photon.Realtime;

namespace RussianLotto.Application
{
    public class IsBecameMasterClientNode : BehaviorNode
    {
        private readonly LoadBalancingClient _photon;

        public IsBecameMasterClientNode(LoadBalancingClient photon)
        {
            _photon = photon;
        }

        public override BehaviorNodeStatus OnExecute(long time)
        {
            return _photon.LocalPlayer.IsMasterClient ? BehaviorNodeStatus.Success : BehaviorNodeStatus.Failure;
        }
    }
}
