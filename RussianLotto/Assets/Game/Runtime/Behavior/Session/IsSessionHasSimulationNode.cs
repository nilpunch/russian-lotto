using BananaParty.BehaviorTree;
using RussianLotto.Client;

namespace RussianLotto.Behavior
{
    public class IsSessionHasSimulationNode : BehaviorNode
    {
        private readonly IReadOnlySession _session;

        public IsSessionHasSimulationNode(IReadOnlySession session)
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
