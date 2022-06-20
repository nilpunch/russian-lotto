namespace RussianLotto.View
{
    public interface ISimulationView
    {
        public IBoardView Board { get; }
        public IHighlightedCellsView HighlightedCells { get; }
        public IAvailableNumbersView AvailableNumbers { get; }
    }
}
