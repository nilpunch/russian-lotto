using BananaParty.BehaviorTree;
using RussianLotto.Client;

namespace RussianLotto.Behavior
{
    public class IsSimulationFinishedNode : BehaviorNode
    {
        private readonly ISession _session;

        public IsSimulationFinishedNode(ISession session)
        {
            _session = session;
        }

        public override BehaviorNodeStatus OnExecute(long time)
        {
            return _session.Simulation.State == SimulationState.Finished ? BehaviorNodeStatus.Success : BehaviorNodeStatus.Failure;
        }
    }
}