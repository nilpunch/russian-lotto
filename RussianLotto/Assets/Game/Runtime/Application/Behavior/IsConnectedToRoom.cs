using BananaParty.BehaviorTree;
using RussianLotto.Networking;

namespace RussianLotto.Application
{
    public class IsConnectedToRoom : BehaviorNode
    {
        private readonly IRoom _room;

        public IsConnectedToRoom(IRoom room)
        {
            _room = room;
        }

        public override BehaviorNodeStatus OnExecute(long time)
        {
            return _room.IsConnectedToRoom ? BehaviorNodeStatus.Success : BehaviorNodeStatus.Failure;
        }
    }
}