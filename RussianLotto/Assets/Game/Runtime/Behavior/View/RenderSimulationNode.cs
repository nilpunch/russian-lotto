using BananaParty.BehaviorTree;
using RussianLotto.Client;
using RussianLotto.View;

namespace RussianLotto.Behavior
{
    public class RenderSimulationNode : BehaviorNode
    {
        private readonly ISimulationView _simulationView;
        private readonly ISession _session;

        public RenderSimulationNode(ISimulationView simulationView, ISession session)
        {
            _simulationView = simulationView;
            _session = session;
        }

        public override BehaviorNodeStatus OnExecute(long time)
        {
            _session.Simulation.Visualize(_simulationView);
            return BehaviorNodeStatus.Success;
        }
    }
}
