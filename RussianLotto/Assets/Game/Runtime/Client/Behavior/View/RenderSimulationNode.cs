using BananaParty.BehaviorTree;
using RussianLotto.View;

namespace RussianLotto.Behavior
{
    public class RenderSimulationNode : BehaviorNode
    {
        private readonly ISimulationView _simulationView;
        private readonly IVisualization<ISimulationView> _simulation;

        public RenderSimulationNode(ISimulationView simulationView, IVisualization<ISimulationView> simulation)
        {
            _simulationView = simulationView;
            _simulation = simulation;
        }

        public override BehaviorNodeStatus OnExecute(long time)
        {
            _simulation.Visualize(_simulationView);
            return BehaviorNodeStatus.Success;
        }
    }
}
