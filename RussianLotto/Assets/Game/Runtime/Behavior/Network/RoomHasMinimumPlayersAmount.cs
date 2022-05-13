using BananaParty.BehaviorTree;
using RussianLotto.Networking;

namespace RussianLotto.Behavior
{
    public class RoomHasMinimumPlayersAmount : BehaviorNode
    {
        private readonly IRoom _room;
        private readonly int _minimumPlayers;

        public RoomHasMinimumPlayersAmount(IRoom room, int minimumPlayers)
        {
            _room = room;
            _minimumPlayers = minimumPlayers;
        }

        public override BehaviorNodeStatus OnExecute(long time)
        {
            return _room.ConnectedPlayers.Count >= _minimumPlayers
                ? BehaviorNodeStatus.Success
                : BehaviorNodeStatus.Failure;
        }
    }
}
