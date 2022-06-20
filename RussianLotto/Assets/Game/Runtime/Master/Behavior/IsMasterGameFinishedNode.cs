using BananaParty.BehaviorTree;

namespace RussianLotto.Master
{
    public class IsMasterGameFinishedNode : BehaviorNode
    {
        private readonly IMasterSimulation _masterRoom;

        public IsMasterGameFinishedNode(IMasterSimulation masterRoom)
        {
            _masterRoom = masterRoom;
        }

        public override BehaviorNodeStatus OnExecute(long time)
        {
            return _masterRoom.MasterRoomState == MasterRoomState.GameFinished ? BehaviorNodeStatus.Success : BehaviorNodeStatus.Failure;
        }
    }
}
