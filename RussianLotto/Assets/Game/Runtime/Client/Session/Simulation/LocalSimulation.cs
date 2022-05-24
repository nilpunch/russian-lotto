using System;
using RussianLotto.Save;
using RussianLotto.View;
using UnityEngine;

namespace RussianLotto.Client
{
    public class LocalSimulation : ISimulation
    {
        private readonly IAvailableNumbers _availableNumbers;
        private readonly IBoard _board;
        private readonly IFactory<ICard> _cardsFactory;

        private readonly IBonuses _bonuses;
        private readonly IWallet _wallet;
        private readonly IHighlightedCells _highlightedCells;
        private readonly IAutomaticMark _availbaleToMark;

        public LocalSimulation(IBonuses bonuses, IBoard board, IFactory<ICard> cardsFactory, IAvailableNumbers availableNumbers)
        {
            _board = board;
            _cardsFactory = cardsFactory;
            _availableNumbers = availableNumbers;
            State = SimulationState.Idle;

            _highlightedCells = new HighlightedCells();
            _availbaleToMark = new AutomaticMark(board, availableNumbers);
            _bonuses = bonuses;
        }

        public SimulationState State { get; private set; }

        public bool IsPlayerWin => _board.IsWin();

        public void ExecuteFrame(long time)
        {
            if (State != SimulationState.Game)
                return;

            _bonuses.MarkMisses.Use(_board);
            _bonuses.Highlight.Use(_highlightedCells);
            _bonuses.AutomaticMark.Use(_availbaleToMark);

            _availableNumbers.ExecuteFrame(time);
            _board.UpdateAllMissingNumbers(_availableNumbers);

            if (_board.IsWin() || _availableNumbers.IsEnded)
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

            State = SimulationState.Finished;
        }

        public void TryMarkCell(int card, Vector2Int cellPosition)
        {
            if (_availableNumbers.IsAvailable(_board.GetNumberAt(card, cellPosition)) && _board.IsAvailable(card, cellPosition))
            {
                _board.Mark(card, cellPosition);
            }
        }

        public void TryChangeCardToNewOne(int cardIndex)
        {
            if (State != SimulationState.Idle)
                return;

            ICard newCard = _cardsFactory.Create();

            _board.ChangeCard(cardIndex, newCard);
        }

        public void Visualize(ISimulationView view)
        {
            _availableNumbers.Visualize(view.AvailableNumbers);
            _board.Visualize(view.Board);
        }
    }
}
