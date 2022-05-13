using BananaParty.BehaviorTree;
using RussianLotto.Client;

namespace RussianLotto.Behavior
{
    public class StartSimulationNode : BehaviorNode
    {
        private readonly ISession _session;

        public StartSimulationNode(ISession session)
        {
            _session = session;
        }

        public override BehaviorNodeStatus OnExecute(long time)
        {
            if (!Started)
            {
                if (!_session.HasSimulation || _session.Simulation.State != SimulationState.Idle)
                    return BehaviorNodeStatus.Failure;

                _session.Simulation.StartGame();
            }

            return BehaviorNodeStatus.Success;
        }
    }
}
