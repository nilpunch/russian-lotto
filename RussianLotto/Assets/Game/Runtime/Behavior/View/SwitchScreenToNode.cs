using BananaParty.BehaviorTree;
using RussianLotto.View;

namespace RussianLotto.Behavior
{
    public class SwitchScreenToNode : BehaviorNode
    {
        private readonly IScreensPresentation _screensPresentation;
        private readonly Screen _screen;

        public SwitchScreenToNode(IScreensPresentation screensPresentation, Screen screen)
        {
            _screensPresentation = screensPresentation;
            _screen = screen;
        }

        public override BehaviorNodeStatus OnExecute(long time)
        {
            if (Started)
                return Status;

            _screensPresentation.SwitchTo(_screen);
            return BehaviorNodeStatus.Success;
        }

        public override string Name => base.Name + " " + _screen.ToString();
    }
}
