using BananaParty.BehaviorTree;

namespace RussianLotto.Master
{
    public class PrepairMasterGameNode : BehaviorNode
    {
        private readonly IMasterSimulation _masterRoom;

        public PrepairMasterGameNode(IMasterSimulation masterRoom)
        {
            _masterRoom = masterRoom;
        }

        public override BehaviorNodeStatus OnExecute(long time)
        {
            if (Started)
                return Status;

            if (_masterRoom.MasterRoomState != MasterRoomState.WaitingPlayers)
                return BehaviorNodeStatus.Failure;

            _masterRoom.PrepareSimulation();

            return BehaviorNodeStatus.Success;
        }
    }
}
