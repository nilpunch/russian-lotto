using System;
using System.Linq;
using RussianLotto.Command;
using UnityEngine;

namespace RussianLotto.Input
{
    public class CardsChangeInput : MonoBehaviour, ICommandInput<ISessionCommand>
    {
        [SerializeField] private CardChangeInput[] _cardChangeInputs;

        private ICommandInput<ISessionCommand> _combinedInput;

        private void Awake()
        {
            _combinedInput = new CommandInputChain<ISessionCommand>(_cardChangeInputs);
        }

        public bool HasUnreadCommands => _combinedInput.HasUnreadCommands;

        public ISessionCommand ReadCommand()
        {
            return _combinedInput.ReadCommand();
        }

        public void Clear()
        {
            _combinedInput.Clear();
        }
    }
}
