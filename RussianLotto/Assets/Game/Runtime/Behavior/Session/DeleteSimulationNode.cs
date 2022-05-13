using BananaParty.BehaviorTree;
using RussianLotto.Client;

namespace RussianLotto.Behavior
{
    public class DeleteSimulationNode : BehaviorNode
    {
        private readonly ISession _session;

        public DeleteSimulationNode(ISession session)
        {
            _session = session;
        }

        public override BehaviorNodeStatus OnExecute(long time)
        {
            if (!_session.HasSimulation)
                return BehaviorNodeStatus.Failure;

            _session.DeleteSimulation();
            return BehaviorNodeStatus.Success;
        }
    }
}
