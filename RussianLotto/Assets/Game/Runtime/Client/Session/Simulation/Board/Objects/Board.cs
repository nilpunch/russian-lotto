using System;
using System.Collections.Generic;
using RussianLotto.View;
using UnityEngine;

namespace RussianLotto.Client
{
    public class Board : IBoard
    {
        private readonly IReadOnlyList<ICard> _cards;
        private int _lastMissedNumber;

        public Board(IReadOnlyList<ICard> cards)
        {
            _cards = cards;
            _lastMissedNumber = -1;
        }

        public bool IsWin()
        {
            foreach (var card in _cards)
            {
                if (card.IsComplete())
                    return true;
            }

            return false;
        }

        public bool IsAvailable(int card, Vector2Int cellPosition)
        {
            return _cards[card].IsAvailable(cellPosition);
        }

        public void Mark(int card, Vector2Int cellPosition)
        {
            if (!IsAvailable(card, cellPosition))
                throw new InvalidOperationException();

            _cards[card].Mark(cellPosition);
        }

        public void UpdateAllMissingNumbers(IReadOnlyAvailableNumbers availableNumbers)
        {
            foreach (int number in availableNumbers.Missed)
            {
                foreach(var card in _cards)
                    if (card.UpdateMissingCells(number))
                        _lastMissedNumber = number;
            }
        }

        public void Visualize(IBoardView view)
        {
            view.DrawCards(_cards);

            if (_lastMissedNumber == -1)
                view.HideLastMissingNumber();
            else
                view.DrawLastMissingNumber(_lastMissedNumber);
        }
    }
}
