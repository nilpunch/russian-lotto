using RussianLotto.Save;

namespace RussianLotto.Client
{
    public interface IBonuses : ISerialization, IDeserialization
    {
        IBonus<IBoard> MarkMisses { get; }
        IBonus<IHighlightedCells> Highlight { get; }
        IBonus<IAutomaticMark> AutomaticMark { get; }
    }
}
