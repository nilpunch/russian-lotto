using BananaParty.BehaviorTree;
using RussianLotto.Client;

namespace RussianLotto.Behavior
{
    public class IsSessionReadyNode : BehaviorNode
    {
        private readonly IReadOnlySession _session;

        public IsSessionReadyNode(IReadOnlySession session)
        {
            _session = session;
        }

        public override BehaviorNodeStatus OnExecute(long time)
        {
            return _session.HasSimulation
                ? BehaviorNodeStatus.Success
                : BehaviorNodeStatus.Failure;
        }
    }
}
