using BananaParty.BehaviorTree;
using RussianLotto.Client;

namespace RussianLotto.Behavior
{
    public class CollectBankNode : BehaviorNode
    {
        private readonly ISession _session;

        public CollectBankNode(ISession session)
        {
            _session = session;
        }

        public override BehaviorNodeStatus OnExecute(long time)
        {
            if (Started)
                return Status;

            _session.Win();

            return BehaviorNodeStatus.Success;
        }
    }
}
