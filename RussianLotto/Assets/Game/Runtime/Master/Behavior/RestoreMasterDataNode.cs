using BananaParty.BehaviorTree;

namespace RussianLotto.Master
{
    public class RestoreMasterDataNode : BehaviorNode
    {
        private readonly IMasterSimulation _masterSimulation;

        public RestoreMasterDataNode(IMasterSimulation masterSimulation)
        {
            _masterSimulation = masterSimulation;
        }

        public override BehaviorNodeStatus OnExecute(long time)
        {
            if (Started)
                return BehaviorNodeStatus.Success;

            _masterSimulation.RestoreStateFromLocal();

            return BehaviorNodeStatus.Success;
        }
    }
}
