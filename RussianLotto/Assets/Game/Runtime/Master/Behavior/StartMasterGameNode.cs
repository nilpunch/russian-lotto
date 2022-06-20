using BananaParty.BehaviorTree;

namespace RussianLotto.Master
{
    public class StartMasterGameNode : BehaviorNode
    {
        private readonly IMasterSimulation _masterRoom;

        public StartMasterGameNode(IMasterSimulation masterRoom)
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
