using BananaParty.BehaviorTree;
using RussianLotto.Command;

namespace RussianLotto.Behavior
{
    public class ClearCommandsNode<TTarget> : BehaviorNode
    {
        private readonly ICommandInput<ICommand<TTarget>> _input;

        public ClearCommandsNode(ICommandInput<ICommand<TTarget>> input)
        {
            _input = input;
        }

        public override BehaviorNodeStatus OnExecute(long time)
        {
            if (Started)
                return Status;

            _input.Clear();

            return BehaviorNodeStatus.Success;
        }
    }
}
