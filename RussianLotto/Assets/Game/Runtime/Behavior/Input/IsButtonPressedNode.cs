using BananaParty.BehaviorTree;
using RussianLotto.Input;

namespace RussianLotto.Behavior
{
    public class IsButtonPressedNode : BehaviorNode
    {
        private readonly IButtonElement _button;

        public IsButtonPressedNode(IButtonElement button)
        {
            _button = button;
        }

        public override BehaviorNodeStatus OnExecute(long time)
        {
            if (_button.Pressed)
            {
                _button.Release();
                return BehaviorNodeStatus.Success;
            }

            return BehaviorNodeStatus.Failure;
        }
    }
}
