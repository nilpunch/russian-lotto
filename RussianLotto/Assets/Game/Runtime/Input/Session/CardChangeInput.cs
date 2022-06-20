using RussianLotto.Command;
using RussianLotto.View;
using UnityEngine;

namespace RussianLotto.Input
{
    public class CardChangeInput : MonoBehaviour, ICommandInput<ISessionCommand>
    {
        [SerializeField] private ButtonInput _buttonInput;
        [SerializeField] private CardView _cardView;

        public bool HasUnreadCommands => _buttonInput.Pressed;

        public ISessionCommand ReadCommand()
        {
            _buttonInput.Release();

            return new ChangeCardCommand(_cardView.CardIndex);
        }

        public void Clear()
        {
            if (_buttonInput.Pressed)
                _buttonInput.Release();
        }
    }
}
