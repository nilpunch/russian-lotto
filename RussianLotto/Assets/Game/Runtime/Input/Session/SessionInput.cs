using RussianLotto.Command;
using UnityEngine;

namespace RussianLotto.Input
{
    public class SessionInput : MonoBehaviour, ISessionInput
    {
        [SerializeField] private BoardInput _boardInput;
        [SerializeField] private CardsChangeInput _cardsChangeInput;
        [SerializeField] private BonusInput _bonusInput;

        private ICommandInput<ISessionCommand> _combinedInput;

        private void Awake()
        {
            _combinedInput = new CommandInputChain<ISessionCommand>(new ICommandInput<ISessionCommand>[]
            {
                _boardInput, _cardsChangeInput, _bonusInput
            });
        }

        public ICommandInput<ISessionCommand> Commands => _combinedInput;
    }
}
