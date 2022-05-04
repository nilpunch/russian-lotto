using BananaParty.BehaviorTree;
using RussianLotto.Networking;

namespace RussianLotto.Behavior
{
    public class IsBecameMasterClientNode : BehaviorNode
    {
        private readonly IMaster _master;

        public IsBecameMasterClientNode(IMaster master)
        {
            _master = master;
        }

        public override BehaviorNodeStatus OnExecute(long time)
        {
            return _master.IsMasterClient ? BehaviorNodeStatus.Success : BehaviorNodeStatus.Failure;
        }
    }
}
