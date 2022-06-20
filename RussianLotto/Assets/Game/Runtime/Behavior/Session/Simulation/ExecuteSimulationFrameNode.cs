using BananaParty.BehaviorTree;
using RussianLotto.Client;

namespace RussianLotto.Behavior
{
    public class ExecuteSimulationFrameNode : BehaviorNode
    {
        private readonly ISession _session;

        public ExecuteSimulationFrameNode(ISession session)
        {
            _session = session;
        }

        public override BehaviorNodeStatus OnExecute(long time)
        {
            _session.Simulation.ExecuteFrame(time);
            return BehaviorNodeStatus.Success;
        }
    }
}
