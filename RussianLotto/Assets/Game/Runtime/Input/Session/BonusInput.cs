using System;
using RussianLotto.Client;
using RussianLotto.Command;
using UnityEngine;

namespace RussianLotto.Input
{
    public class BonusInput : MonoBehaviour, ICommandInput<ISessionCommand>
    {
        [SerializeField] private ButtonInput _highlightAvailableButton;
        [SerializeField] private ButtonInput _markMissesButton;
        [SerializeField] private ButtonInput _automaticMarkButton;

        public bool HasUnreadCommands => _highlightAvailableButton.Pressed ||
                                         _markMissesButton.Pressed ||
                                         _automaticMarkButton.Pressed;

        public ISessionCommand ReadCommand()
        {
            if (_highlightAvailableButton.Pressed)
            {
                _highlightAvailableButton.Release();
                return new TopUpBonusCommand(BonusType.HighlightAvailable);
            }

            if (_markMissesButton.Pressed)
            {
                _markMissesButton.Release();
                return new TopUpBonusCommand(BonusType.MarkMisses);
            }

            if (_automaticMarkButton.Pressed)
            {
                _automaticMarkButton.Release();
                return new TopUpBonusCommand(BonusType.AutomaticMark);
            }

            throw new InvalidOperationException();
        }

        public void Clear()
        {
            if (_highlightAvailableButton.Pressed)
                _highlightAvailableButton.Release();

            if (_markMissesButton.Pressed)
                _markMissesButton.Release();

            if (_automaticMarkButton.Pressed)
                _automaticMarkButton.Release();
        }
    }
}
