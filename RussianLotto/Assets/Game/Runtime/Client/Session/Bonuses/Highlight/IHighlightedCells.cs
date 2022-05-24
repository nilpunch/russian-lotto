using RussianLotto.View;

namespace RussianLotto.Client
{
    public interface IHighlightedCells : IVisualization<IHighlightedCellsView>
    {
        public int HighlightsAvailable { get; }
        public void Highlight(int amount);
    }
}
