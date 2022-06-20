using BananaParty.BehaviorTree;

namespace RussianLotto.Master
{
    public class ResetMasterGameNode : BehaviorNode
    {
        private readonly IMasterSimulation _masterRoom;

        public ResetMasterGameNode(IMasterSimulation masterRoom)
        {
            _masterRoom = masterRoom;
        }

        public override BehaviorNodeStatus OnExecute(long time)
        {
            if (Started)
                return Status;

            _masterRoom.ResetSimulation();

            return BehaviorNodeStatus.Success;
        }
    }
}
