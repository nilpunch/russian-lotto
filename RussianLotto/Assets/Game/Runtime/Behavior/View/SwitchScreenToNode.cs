using BananaParty.BehaviorTree;
using RussianLotto.View;

namespace RussianLotto.Behavior
{
    public class SwitchScreenToNode : BehaviorNode
    {
        private readonly IPresentation _presentation;
        private readonly Screen _screen;

        public SwitchScreenToNode(IPresentation presentation, Screen screen)
        {
            _presentation = presentation;
            _screen = screen;
        }

        public override BehaviorNodeStatus OnExecute(long time)
        {
            _presentation.SwitchTo(_screen);
            return BehaviorNodeStatus.Success;
        }

        public override string Name => base.Name + " " + _screen.ToString();
    }
}
