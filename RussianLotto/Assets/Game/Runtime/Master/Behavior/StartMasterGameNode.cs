using BananaParty.BehaviorTree;

namespace RussianLotto.Master
{
    public class StartMasterGameNode : BehaviorNode
    {
        private readonly MasterSimulation _masterRoom;

        public StartMasterGameNode(MasterSimulation masterRoom)
        {
            _masterRoom = masterRoom;
        }

        public override BehaviorNodeStatus OnExecute(long time)
        {
            if (Started)
                return Status;

            if (_masterRoom.MasterRoomState != MasterRoomState.GamePreparation)
                return BehaviorNodeStatus.Failure;

            _masterRoom.StartSimulation();

            return BehaviorNodeStatus.Success;
        }
    }
}
