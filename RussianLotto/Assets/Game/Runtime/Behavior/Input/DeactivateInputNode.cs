using BananaParty.BehaviorTree;
using RussianLotto.Input;

namespace RussianLotto.Behavior
{
    public class DeactivateInputNode : BehaviorNode
    {
        private readonly IInputElement _inputElement;

        public DeactivateInputNode(IInputElement inputElement)
        {
            _inputElement = inputElement;
        }

        public override BehaviorNodeStatus OnExecute(long time)
        {
            if (Started)
                return Status;

            _inputElement.Active = false;
            return BehaviorNodeStatus.Success;
        }
    }
}
