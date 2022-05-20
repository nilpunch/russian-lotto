using BananaParty.BehaviorTree;
using RussianLotto.Networking;

namespace RussianLotto.Master
{
    public class ResetMasterGameNode : BehaviorNode
    {
        private readonly MasterSimulation _masterRoom;

        public ResetMasterGameNode(MasterSimulation masterRoom)
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
