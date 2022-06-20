using RussianLotto.Client;

namespace RussianLotto.Save
{
    public interface IBonusesSave
    {
        ISave<IBonus<IHighlightedCells>> HighlightBonusSave { get; }
        ISave<IBonus<IBoard>> MarkMissesBonusSave { get; }
        ISave<IBonus<IAutomaticMark>> AutomaticMarkBonusSave { get; }
    }
}
