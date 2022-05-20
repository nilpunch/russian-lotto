using BananaParty.BehaviorTree;
using RussianLotto.Client;

namespace RussianLotto.Behavior
{
    public class IsSimulationPlayingNode : BehaviorNode
    {
        private readonly ISession _session;

        public IsSimulationPlayingNode(ISession session)
        {
            _session = session;
        }

        public override BehaviorNodeStatus OnExecute(long time)
        {
            return _session.Simulation.State == SimulationState.Game ? BehaviorNodeStatus.Success : BehaviorNodeStatus.Failure;
        }
    }
}
