using BananaParty.BehaviorTree;
using RussianLotto.Networking;

namespace RussianLotto.Application
{
    public class IsConnectedToRoom : BehaviorNode
    {
        private readonly IRoomNetwork _roomNetwork;

        public IsConnectedToRoom(IRoomNetwork roomNetwork)
        {
            _roomNetwork = roomNetwork;
        }

        public override BehaviorNodeStatus OnExecute(long time)
        {
            return _roomNetwork.IsConnectedToRoom ? BehaviorNodeStatus.Success : BehaviorNodeStatus.Failure;
        }
    }
}