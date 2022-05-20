namespace RussianLotto.Client
{
    public interface IBonuses
    {
        IBonus<IBoard> MarkMisses { get; }
        IBonus<IHighlightedCells> HighlightAvailable { get; }
        IBonus<IAvailableToMarkCells> AutomaticMark { get; }

        void Use();
    }
}
