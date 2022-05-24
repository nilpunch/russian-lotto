using BananaParty.BehaviorTree;
using RussianLotto.Client;
using RussianLotto.Networking;

namespace RussianLotto.Behavior
{
    public class MultiplyBetByPlayersAmountNode : BehaviorNode
    {
        private readonly ISession _session;
        private readonly IRoom _room;

        public MultiplyBetByPlayersAmountNode(ISession session, IRoom room)
        {
            _session = session;
            _room = room;
        }

        public override BehaviorNodeStatus OnExecute(long time)
        {
            if (Started)
                return Status;

            _session.MultiplyBet(_room.ConnectedPlayers.Count);

            return BehaviorNodeStatus.Success;
        }
    }
}
