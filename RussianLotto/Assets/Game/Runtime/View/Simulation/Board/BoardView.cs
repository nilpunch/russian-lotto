using System.Collections.Generic;
using System.Linq;
using RussianLotto.Client;
using UnityEngine;

namespace RussianLotto.View
{
    public class BoardView : MonoBehaviour, IBoardView, IHighlightedCellsView
    {
        [SerializeField] private NumberView _missingNumberView;
        [SerializeField] private CardView[] _cards;

        private void Awake()
        {
            for (int i = 0; i < _cards.Length; ++i)
                _cards[i].CardIndex = i;
        }

        public void DrawCards(IReadOnlyCollection<IReadOnlyCard> cards)
        {
            foreach ((IReadOnlyCard card, CardView view) in cards.Zip(_cards, (card, view) => (card, view)))
                view.DrawCells(card.ReadOnlyCells);
        }

        public void DrawLastMissingNumber(int lastMissingNumber)
        {
            _missingNumberView.Show(lastMissingNumber);
        }

        public void HideLastMissingNumber()
        {
            _missingNumberView.Hide();
        }

        public void ShowHighlight(IEnumerable<(int, Vector2Int)> cardCells)
        {
            foreach (var card in _cards)
            {
                foreach (var cellView in card.Cells)
                {
                    cellView.Highlight(false);
                }
            }

            foreach (var (highlightCard, highlightCell) in cardCells)
            {
                _cards[highlightCard].Cells.First(cell => cell.CellPosition == highlightCell).Highlight(true);
            }
        }
    }
}
