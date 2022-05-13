using RussianLotto.Command;
using UnityEngine;

namespace RussianLotto.Input
{
    public class SessionInput : MonoBehaviour, ISessionInput
    {
        [SerializeField] private BoardInput _boardInput;

        public ICommandInput<ISessionCommand> Commands => _boardInput;
    }
}
