using BananaParty.BehaviorTree;

namespace RussianLotto.Master
{
    public class IsMasterGameStartedNode : BehaviorNode
    {
        private readonly MasterSimulation _masterRoom;

        public IsMasterGameStartedNode(MasterSimulation masterRoom)
        {
            _masterRoom = masterRoom;
        }

        public override BehaviorNodeStatus OnExecute(long time)
        {
            return _masterRoom.MasterRoomState == MasterRoomState.GameSimulation ? BehaviorNodeStatus.Success : BehaviorNodeStatus.Failure;
        }
    }
}
