namespace RussianLotto.Client
{
    public interface IBoardCommand
    {
        void Execute(IBoard board);
    }
}
