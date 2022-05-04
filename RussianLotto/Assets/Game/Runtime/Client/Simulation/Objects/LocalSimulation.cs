using RussianLotto.Networking;
using RussianLotto.View;

namespace RussianLotto.Client
{
    public class LocalSimulation : ISimulation
    {
        private readonly IBoard _board;
        private readonly IAvailableNumbers _availableNumbers;

        public LocalSimulation(IBoard board, IAvailableNumbers availableNumbers)
        {
            _board = board;
            _availableNumbers = availableNumbers;
            GameState = GameState.Idle;
        }

        public GameState GameState { get; private set; }

        public void ExecuteFrame(long time)
        {
            _availableNumbers.ExecuteFrame(time);
            _board.UpdateAllMissingNumbers(_availableNumbers);
        }

        public void StartGame()
        {

        }

        public void Visualize(ISimulationView view)
        {
            _availableNumbers.Visualize(view.AvailableNumbers);
            _board.Visualize(view.Board);
        }
    }
}
