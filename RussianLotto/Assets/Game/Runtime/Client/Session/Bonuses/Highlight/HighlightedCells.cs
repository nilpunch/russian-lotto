using System.Collections.Generic;
using System.Linq;
using RussianLotto.View;
using UnityEngine;

namespace RussianLotto.Client
{
    public class HighlightedCells : IHighlightedCells
    {
        private readonly IBoard _board;
        private readonly IReadOnlyAvailableNumbers _availableNumbers;

        private readonly List<(int, Vector2Int)> _highlightedCells;

        public HighlightedCells(IBoard board, IReadOnlyAvailableNumbers availableNumbers)
        {
            _board = board;
            _availableNumbers = availableNumbers;
            _highlightedCells = new List<(int, Vector2Int)>();
        }

        public int HighlightsAvailable => _board.AllAvailableToMarkCells(_availableNumbers).Except(_highlightedCells).Count();

        public void Highlight(int amount)
        {
            int addsLeft = amount;

            foreach ((int card, Vector2Int cell) combo in _board.AllAvailableToMarkCells(_availableNumbers))
            {
                if (_highlightedCells.Contains(combo))
                    continue;

                _highlightedCells.Add(combo);
                addsLeft -= 1;

                if (addsLeft == 0)
                    return;
            }
        }

        public void Visualize(IHighlightedCellsView view)
        {
            view.ShowHighlight(_highlightedCells.Where(combo => _board.IsAvailable(combo.Item1, combo.Item2)));
        }
    }
}
