using BananaParty.BehaviorTree;
using RussianLotto.Networking;

namespace RussianLotto.Behavior
{
    public class IsRoomOpenToJoiningNode : BehaviorNode
    {
        private readonly IRoom _room;

        public IsRoomOpenToJoiningNode(IRoom room)
        {
            _room = room;
        }

        public override BehaviorNodeStatus OnExecute(long time)
        {
            return _room.IsOpenToJoin ? BehaviorNodeStatus.Success : BehaviorNodeStatus.Failure;
        }
    }
}
