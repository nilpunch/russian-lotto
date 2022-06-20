using System.Collections.Generic;
using System.Linq;

namespace RussianLotto.Command
{
    public class CommandInputChain<T> : ICommandInput<T>
    {
        private readonly IEnumerable<ICommandInput<T>> _inputs;

        public CommandInputChain(IEnumerable<ICommandInput<T>> inputs)
        {
            _inputs = inputs;
        }

        public bool HasUnreadCommands => _inputs.Any(input => input.HasUnreadCommands);

        public T ReadCommand()
        {
            return _inputs.First(input => input.HasUnreadCommands).ReadCommand();
        }

        public void Clear()
        {
            foreach (ICommandInput<T> commandInput in _inputs)
                commandInput.Clear();
        }
    }
}
