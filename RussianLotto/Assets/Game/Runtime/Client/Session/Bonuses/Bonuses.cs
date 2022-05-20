namespace RussianLotto.Client
{
    public class Bonuses : IBonuses
    {
        private readonly IBoard _board;
        private readonly HighlightedCells _highlightedCells;
        private readonly AvailableToMarkCells _availbaleToMark;

        public Bonuses(IBoard board, IReadOnlyAvailableNumbers availableNumbers)
        {
            _board = board;

            _highlightedCells = new HighlightedCells();
            _availbaleToMark = new AvailableToMarkCells(board, availableNumbers);

            MarkMisses = new MarkMissesBonus(0);
            HighlightAvailable = new HighlightAvailableBonus(0);
            AutomaticMark = new AutomaticMarkBonus(0);
        }

        public IBonus<IBoard> MarkMisses { get; }
        public IBonus<IHighlightedCells> HighlightAvailable { get; }
        public IBonus<IAvailableToMarkCells> AutomaticMark { get; }

        public void Use()
        {
            MarkMisses.Use(_board);
            HighlightAvailable.Use(_highlightedCells);
            AutomaticMark.Use(_availbaleToMark);
        }
    }
}
