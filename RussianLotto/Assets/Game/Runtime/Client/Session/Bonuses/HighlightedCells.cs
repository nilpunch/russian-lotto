using RussianLotto.View;

namespace RussianLotto.Client
{
    public class HighlightedCells : IHighlightedCells
    {
        public int HighlightsAvailable => 0;

        public void Highlight(int amount)
        {
        }

        public void Visualize(IHighlightedCellsView view)
        {
        }
    }
}
