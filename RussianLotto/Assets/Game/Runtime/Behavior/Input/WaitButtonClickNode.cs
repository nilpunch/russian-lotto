using BananaParty.BehaviorTree;
using RussianLotto.Input;

namespace RussianLotto.Behavior
{
    public class WaitButtonClickNode : BehaviorNode
    {
        private readonly IButtonElement _button;

        public WaitButtonClickNode(IButtonElement button)
        {
            _button = button;
        }

        public override BehaviorNodeStatus OnExecute(long time)
        {
            if (Status == BehaviorNodeStatus.Success)
                return BehaviorNodeStatus.Success;

            if (_button.Pressed == false)
                return BehaviorNodeStatus.Running;

            _button.Release();
            return BehaviorNodeStatus.Success;
        }

        public override void Reset()
        {
            base.Reset();

            if (_button.Pressed)
                _button.Release();
        }
    }
}
