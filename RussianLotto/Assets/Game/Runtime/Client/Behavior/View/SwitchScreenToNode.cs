using BananaParty.BehaviorTree;
using RussianLotto.View;

namespace RussianLotto.Behavior
{
    public class SwitchScreenToNode : BehaviorNode
    {
        private readonly IVisibleScreen _visibleScreen;
        private readonly Screen _screen;

        public SwitchScreenToNode(IVisibleScreen visibleScreen, Screen screen)
        {
            _visibleScreen = visibleScreen;
            _screen = screen;
        }

        public override BehaviorNodeStatus OnExecute(long time)
        {
            _visibleScreen.SwitchTo(_screen);
            return BehaviorNodeStatus.Success;
        }
    }
}
