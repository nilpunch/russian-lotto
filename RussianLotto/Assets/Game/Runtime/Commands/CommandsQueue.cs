using System.Collections.Generic;

namespace RussianLotto.Command
{
    public class CommandsQueue<T> : ICommandInput<T>
    {
        private readonly Queue<T> _commandsQueue = new();

        public bool HasUnreadCommands => _commandsQueue.Count > 0;

        public T ReadCommand() => _commandsQueue.Dequeue();

        public void PushCommand(T command)
        {
            _commandsQueue.Enqueue(command);
        }
    }
}
