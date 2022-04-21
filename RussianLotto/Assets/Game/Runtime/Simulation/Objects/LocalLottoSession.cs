namespace RussianLotto.Client
{
    public class LocalLottoSession : IGameLoop
    {
        private readonly IBoard _board;
        private readonly IAvailableNumbers _availableNumbers;

        public LocalLottoSession(IBoard board, IAvailableNumbers availableNumbers)
        {
            _board = board;
            _availableNumbers = availableNumbers;
        }

        public void ExecuteFrame(long time)
        {
            _availableNumbers.ExecuteFrame(time);
            _board.UpdateAllMissingNumbers(_availableNumbers);
        }
    }
}
