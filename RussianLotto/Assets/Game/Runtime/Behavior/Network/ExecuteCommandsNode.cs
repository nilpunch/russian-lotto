using BananaParty.BehaviorTree;
using RussianLotto.Command;

namespace RussianLotto.Behavior
{
    public class ExecuteCommandsNode<TTarget> : BehaviorNode
    {
        private readonly ICommandInput<ICommand<TTarget>> _input;
        private readonly TTarget _target;
        private readonly string _inputName;

        public ExecuteCommandsNode(ICommandInput<ICommand<TTarget>> input, TTarget target, string inputName)
        {
            _input = input;
            _target = target;
            _inputName = inputName;
        }

        public override BehaviorNodeStatus OnExecute(long time)
        {
            while (_input.HasUnreadCommands)
            {
                _input.ReadCommand().Execute(_target);
            }

            return BehaviorNodeStatus.Success;
        }

        public override string Name => base.Name + " From " + _inputName + " To " + typeof(TTarget).Name;
    }
}
