using BananaParty.BehaviorTree;
using RussianLotto.Networking;

namespace RussianLotto.Behavior
{
    public class IsEnteredRoom : BehaviorNode
    {
        private readonly IRoom _room;

        public IsEnteredRoom(IRoom room)
        {
            _room = room;
        }

        public override BehaviorNodeStatus OnExecute(long time)
        {
            return _room.IsEntered ? BehaviorNodeStatus.Success : BehaviorNodeStatus.Failure;
        }
    }
}
