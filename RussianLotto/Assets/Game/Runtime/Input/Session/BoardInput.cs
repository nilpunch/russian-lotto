using RussianLotto.Command;
using UnityEngine;

namespace RussianLotto.Input
{
    public class BoardInput : CommandInputQueue<ISessionCommand>
    {
        [SerializeField] private CardCellClickInput[] _cardInputs;

        protected override void Awake()
        {
            base.Awake();

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
            PushCommand(new MarkCellCommand(cardIndex, cellPosition));
        }
    }
}
