using RussianLotto.Command;
using UnityEngine;

namespace RussianLotto.Input
{
    public class BoardInput : MonoBehaviour, ICommandInput<ISessionCommand>
    {
        [SerializeField] private CardInput[] _cardInputs;

        private CommandsQueue<ISessionCommand> _payload;

        private void Awake()
        {
            _payload = new CommandsQueue<ISessionCommand>();

            foreach (var card in _cardInputs)
                card.Clicked += OnCellClicked;
        }

        private void OnDestroy()
        {
            foreach (var card in _cardInputs)
                card.Clicked -= OnCellClicked;
        }

        private void OnCellClicked(int cardIndex, Vector2Int cellPosition)
        {
            _payload.PushCommand(new MarkCellCommand(cardIndex, cellPosition));
        }

        public bool HasUnreadCommands => _payload.HasUnreadCommands;
        public ISessionCommand ReadCommand()
        {
            return _payload.ReadCommand();
        }
    }
}
