using BananaParty.BehaviorTree;
using RussianLotto.Input;

namespace RussianLotto.Behavior
{
    public class IsButtonPressedNode : BehaviorNode
    {
        private readonly IButtonElement _button;
        private readonly bool _noRelease;

        public IsButtonPressedNode(IButtonElement button, bool noRelease = false)
        {
            _button = button;
            _noRelease = noRelease;
        }

        public override BehaviorNodeStatus OnExecute(long time)
        {
            if (_button.Pressed)
            {
                if (!_noRelease)
                    _button.Release();
                return BehaviorNodeStatus.Success;
            }

            return BehaviorNodeStatus.Failure;
        }
    }
}
