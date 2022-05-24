using RussianLotto.Save;

namespace RussianLotto.Client
{
    public class Bonuses : IBonuses
    {
        private readonly IBonusesSave _bonusesSave;
        private readonly IBoard _board;
        private readonly HighlightedCells _highlightedCells;
        private readonly AutomaticMark _availbaleToMark;

        public Bonuses()
        {
            MarkMisses = new MarkMissesBonus();
            Highlight = new HighlightBonus();
            AutomaticMark = new AutomaticMarkBonus();
        }

        public IBonus<IBoard> MarkMisses { get; }
        public IBonus<IHighlightedCells> Highlight { get; }
        public IBonus<IAutomaticMark> AutomaticMark { get; }

        public void Serialize(IWriteHandle writeHandle)
        {
            MarkMisses.Serialize(writeHandle);
            Highlight.Serialize(writeHandle);
            AutomaticMark.Serialize(writeHandle);
        }

        public void Deserialize(IReadHandle readHandle)
        {
            MarkMisses.Deserialize(readHandle);
            Highlight.Deserialize(readHandle);
            AutomaticMark.Deserialize(readHandle);
        }
    }
}
