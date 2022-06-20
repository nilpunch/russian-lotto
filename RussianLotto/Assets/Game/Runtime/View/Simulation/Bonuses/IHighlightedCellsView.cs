using System.Collections.Generic;
using UnityEngine;

namespace RussianLotto.View
{
    public interface IHighlightedCellsView
    {
        public void ShowHighlight(IEnumerable<(int, Vector2Int)> cardCells);
    }
}
