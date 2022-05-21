using BananaParty.BehaviorTree;
using RussianLotto.Networking;

namespace RussianLotto.Behavior
{
    public class RoomHasMinPlayersAmountToStartNode : BehaviorNode
    {
        private readonly IRoom _room;

        public RoomHasMinPlayersAmountToStartNode(IRoom room)
        {
            _room = room;
        }

        public override BehaviorNodeStatus OnExecute(long time)
        {
            return _room.ConnectedPlayers.Count >= _room.MinPlayersAmountToStart
                ? BehaviorNodeStatus.Success
                : BehaviorNodeStatus.Failure;
        }
    }
}
