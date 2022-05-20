using System;
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

        public LocalSimulation(IBoard board, IFactory<ICard> cardsFactory, IAvailableNumbers availableNumbers)
        {
            _board = board;
            _cardsFactory = cardsFactory;
            _availableNumbers = availableNumbers;
            State = SimulationState.Idle;

            _bonuses = new Bonuses(board, availableNumbers);
        }

        public SimulationState State { get; private set; }

        public bool IsPlayerWin => _board.IsWin();

        public void ExecuteFrame(long time)
        {
            if (State != SimulationState.Game)
                return;

            _bonuses.Use();

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

        public void TopUpAutomaticMarkBonus()
        {
            _bonuses.AutomaticMark.TopUp(3);
        }

        public void TopUpHighlithAvailableBonus()
        {
            _bonuses.HighlightAvailable.TopUp(6);
        }

        public void TopUpMarkMissesBonus()
        {
            _bonuses.MarkMisses.TopUp(1);
        }

        public void ChangeCardToNewOne(int cardIndex)
        {
            if (State != SimulationState.Idle)
                throw new InvalidOperationException();

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
