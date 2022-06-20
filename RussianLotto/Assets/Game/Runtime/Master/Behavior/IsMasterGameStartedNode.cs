using BananaParty.BehaviorTree;

namespace RussianLotto.Master
{
    public class IsMasterGameStartedNode : BehaviorNode
    {
        private readonly IMasterSimulation _masterRoom;

        public IsMasterGameStartedNode(IMasterSimulation masterRoom)
        {
            _masterRoom = masterRoom;
        }

        public override BehaviorNodeStatus OnExecute(long time)
        {
            return _masterRoom.MasterRoomState == MasterRoomState.GameSimulation ? BehaviorNodeStatus.Success : BehaviorNodeStatus.Failure;
        }
    }
}
