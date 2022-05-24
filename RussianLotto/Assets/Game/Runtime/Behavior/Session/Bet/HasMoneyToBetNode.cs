using BananaParty.BehaviorTree;
using RussianLotto.Client;
using RussianLotto.Input;

namespace RussianLotto.Behavior
{
    public class HasMoneyToBetNode : BehaviorNode
    {
        private readonly ISession _session;
        private readonly ISwitch<int> _bet;

        public HasMoneyToBetNode(ISession session, ISwitch<int> bet)
        {
            _session = session;
            _bet = bet;
        }

        public override BehaviorNodeStatus OnExecute(long time)
        {
            return _session.CanBet(_bet.State) ? BehaviorNodeStatus.Success : BehaviorNodeStatus.Failure;
        }
    }
}
