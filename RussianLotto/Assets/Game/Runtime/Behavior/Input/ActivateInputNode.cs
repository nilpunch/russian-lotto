using BananaParty.BehaviorTree;
using RussianLotto.Input;

namespace RussianLotto.Behavior
{
    public class ActivateInputNode : BehaviorNode
    {
        private readonly IInputElement _inputElement;

        public ActivateInputNode(IInputElement inputElement)
        {
            _inputElement = inputElement;
        }

        public override BehaviorNodeStatus OnExecute(long time)
        {
            if (Started)
                return Status;

            _inputElement.Active = true;
            return BehaviorNodeStatus.Success;
        }
    }
}
