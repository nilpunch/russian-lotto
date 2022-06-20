using BananaParty.BehaviorTree;
using RussianLotto.Client;
using RussianLotto.Input;

namespace RussianLotto.Behavior
{
    public class BetNode : BehaviorNode
    {
        private readonly ISession _session;
        private readonly ISwitch<int> _bet;

        public BetNode(ISession session, ISwitch<int> bet)
        {
            _session = session;
            _bet = bet;
        }

        public override BehaviorNodeStatus OnExecute(long time)
        {
            if (Started)
                return Status;

            if (_session.CanBet(_bet.State))
            {
                _session.Bet(_bet.State);
                return BehaviorNodeStatus.Success;
            }

            return BehaviorNodeStatus.Failure;
        }
    }
}
