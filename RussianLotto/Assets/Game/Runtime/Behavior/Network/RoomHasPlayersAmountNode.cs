using BananaParty.BehaviorTree;
using RussianLotto.Networking;

namespace RussianLotto.Behavior
{
    public class RoomHasPlayersAmountNode : BehaviorNode
    {
        private readonly IRoom _room;
        private readonly int _playersAmount;

        public RoomHasPlayersAmountNode(IRoom room, int playersAmount)
        {
            _room = room;
            _playersAmount = playersAmount;
        }

        public override BehaviorNodeStatus OnExecute(long time)
        {
            return _room.ConnectedPlayers.Count >= _playersAmount
                ? BehaviorNodeStatus.Success
                : BehaviorNodeStatus.Failure;
        }
    }
}
