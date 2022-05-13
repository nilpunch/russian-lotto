using RussianLotto.Command;
using RussianLotto.Input;
using UnityEngine;

namespace RussianLotto.Input
{
    public class SessionInput : MonoBehaviour, ISessionInput
    {
        public ICommandInput<ISessionCommand> Commands => new CommandsQueue<ISessionCommand>();
    }
}
