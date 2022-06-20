using BananaParty.BehaviorTree;

namespace RussianLotto.Master
{
    public class IsMasterGamePreparationNode : BehaviorNode
    {
        private readonly IMasterSimulation _masterRoom;

        public IsMasterGamePreparationNode(IMasterSimulation masterRoom)
        {
            _masterRoom = masterRoom;
        }

        public override BehaviorNodeStatus OnExecute(long time)
        {
            return _masterRoom.MasterRoomState == MasterRoomState.GamePreparation ? BehaviorNodeStatus.Success : BehaviorNodeStatus.Failure;
        }
    }
}
