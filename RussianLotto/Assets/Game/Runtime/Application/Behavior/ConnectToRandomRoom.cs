using BananaParty.BehaviorTree;
using RussianLotto.Networking;

namespace RussianLotto.Application
{
    public class ConnectToRandomRoom : BehaviorNode
    {
        private readonly IRoomNetwork _roomNetwork;

        public ConnectToRandomRoom(IRoomNetwork roomNetwork)
        {
            _roomNetwork = roomNetwork;
        }

        public override BehaviorNodeStatus OnExecute(long time)
        {
            if (!Started)
            {
                _roomNetwork.ConnectToRandomRoom();
            }

            return _roomNetwork.IsConnectedToRoom ? BehaviorNodeStatus.Success : BehaviorNodeStatus.Running;
        }
    }
}