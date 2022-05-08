using System;
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
            State = SimulationState.Idle;
        }

        public SimulationState State { get; private set; }

        public void ExecuteFrame(long time)
        {
            if (State != SimulationState.Game)
                return;

            _availableNumbers.ExecuteFrame(time);
            _board.UpdateAllMissingNumbers(_availableNumbers);

            if (_board.IsWin())
                FinishGame();
        }

        public void StartGame()
        {
            if (State != SimulationState.Idle)
                throw new InvalidOperationException();

            State = SimulationState.Game;
        }

        public void FinishGame()
        {
            if (State != SimulationState.Game)
                throw new InvalidOperationException();

            State = SimulationState.Finish;
        }

        public void Visualize(ISimulationView view)
        {
            _availableNumbers.Visualize(view.AvailableNumbers);
            _board.Visualize(view.Board);
        }
    }
}
