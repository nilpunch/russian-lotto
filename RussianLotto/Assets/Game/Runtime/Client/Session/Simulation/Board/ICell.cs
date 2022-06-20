namespace RussianLotto.Client
{
    public interface ICell : IReadOnlyCell
    {
        void Miss();
        void Mark();
        void Restore();
    }
}
