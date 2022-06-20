using BananaParty.BehaviorTree;
using RussianLotto.Client;

namespace RussianLotto.Behavior
{
    public class LoseBankNode : BehaviorNode
    {
        private readonly ISession _session;

        public LoseBankNode(ISession session)
        {
            _session = session;
        }

        public override BehaviorNodeStatus OnExecute(long time)
        {
            if (Started)
                return Status;

            _session.Lose();

            return BehaviorNodeStatus.Success;
        }
    }
}
