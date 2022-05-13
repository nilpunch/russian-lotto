using BananaParty.BehaviorTree;
using RussianLotto.Networking;

namespace RussianLotto.Behavior
{
    public class ExitRoomNode : BehaviorNode
    {
        private readonly IRoom _room;

        public ExitRoomNode(IRoom room)
        {
            _room = room;
        }

        public override BehaviorNodeStatus OnExecute(long time)
        {
            if (!Started)
            {
                _room.Exit();
            }

            return _room.IsEntered == false ? BehaviorNodeStatus.Success : BehaviorNodeStatus.Running;
        }
    }
}
