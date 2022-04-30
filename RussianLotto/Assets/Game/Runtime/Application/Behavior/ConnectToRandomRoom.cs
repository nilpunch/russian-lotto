using BananaParty.BehaviorTree;
using RussianLotto.Networking;

namespace RussianLotto.Application
{
    public class ConnectToRandomRoom : BehaviorNode
    {
        private readonly IRoom _room;

        public ConnectToRandomRoom(IRoom room)
        {
            _room = room;
        }

        public override BehaviorNodeStatus OnExecute(long time)
        {
            if (!Started)
            {
                _room.ConnectToRandomRoom();
            }

            return _room.IsConnectedToRoom ? BehaviorNodeStatus.Success : BehaviorNodeStatus.Running;
        }
    }
}