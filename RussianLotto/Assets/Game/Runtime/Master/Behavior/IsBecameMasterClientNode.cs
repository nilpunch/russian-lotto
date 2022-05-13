using BananaParty.BehaviorTree;
using RussianLotto.Networking;

namespace RussianLotto.Master
{
    public class IsBecameMasterClientNode : BehaviorNode
    {
        private readonly IMasterNetwork _master;

        public IsBecameMasterClientNode(IMasterNetwork master)
        {
            _master = master;
        }

        public override BehaviorNodeStatus OnExecute(long time)
        {
            return _master.IsMasterClient ? BehaviorNodeStatus.Success : BehaviorNodeStatus.Failure;
        }
    }
}
