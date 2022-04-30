using BananaParty.BehaviorTree;
using Photon.Realtime;
using RussianLotto.Networking;

namespace RussianLotto.Application
{
    public class IsBecameMasterClientNode : BehaviorNode
    {
        private readonly IMasterNetwork _masterNetwork;

        public IsBecameMasterClientNode(IMasterNetwork masterNetwork)
        {
            _masterNetwork = masterNetwork;
        }

        public override BehaviorNodeStatus OnExecute(long time)
        {
            return _masterNetwork.IsMasterClient ? BehaviorNodeStatus.Success : BehaviorNodeStatus.Failure;
        }
    }
}
