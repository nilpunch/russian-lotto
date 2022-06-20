using RussianLotto.Command;
using UnityEngine;

namespace RussianLotto.Input
{
    public abstract class CommandInputQueue<T> : MonoBehaviour, ICommandInput<T>
    {
        private CommandsQueue<T> _payload;

        protected virtual void Awake()
        {
            _payload = new CommandsQueue<T>();
        }

        public bool HasUnreadCommands => _payload.HasUnreadCommands;

        public T ReadCommand()
        {
            return _payload.ReadCommand();
        }

        public void Clear()
        {
            _payload.Clear();
        }

        protected void PushCommand(T command)
        {
            _payload.PushCommand(command);
        }
    }
}
