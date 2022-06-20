using System;
using System.Collections.Generic;
using System.Linq;
using RussianLotto.View;
using UnityEngine;

namespace RussianLotto.Client
{
    public class Board : IBoard
    {
        private readonly ICard[] _cards;
        private int _lastMissedNumber;

        public Board(IReadOnlyList<ICard> cards)
        {
            _cards = cards.ToArray();
            _lastMissedNumber = -1;
        }

        public bool IsWin()
        {
            return _cards.Any(card => card.IsComplete());
        }

        public int MissesAmount =>
            _cards.SelectMany(card => card.ReadOnlyCells).Count(cell => cell.Status == CellStatus.Missed);

        public int AvailableToMark(IReadOnlyAvailableNumbers availableNumbers)
        {
            int count = 0;

            foreach (int number in availableNumbers.Available)
                foreach (var card in _cards)
                    count += card.AvailableCellsWithNumber(number);

            return count;
        }

        public IEnumerable<(int, Vector2Int)> AllAvailableToMarkCells(IReadOnlyAvailableNumbers availableNumbers)
        {
            foreach (int number in availableNumbers.Available)
            {
                for (var index = 0; index < _cards.Length; index++)
                {
                    var card = _cards[index];
                    foreach (var cellPosition in card.AllAvailableCellsWithNumber(number))
                    {
                        yield return (index, cellPosition);
                    }
                }
            }
        }

        public bool IsAvailable(int card, Vector2Int cellPosition)
        {
            return _cards[card].IsAvailable(cellPosition);
        }

        public void Mark(int cardIndex, Vector2Int cellPosition)
        {
            if (!IsAvailable(cardIndex, cellPosition))
                throw new InvalidOperationException();

            _cards[cardIndex].Mark(cellPosition);
        }

        public int GetNumberAt(int cardIndex, Vector2Int cellPosition)
        {
            return _cards[cardIndex].ReadOnlyCells.FirstOrDefault(cell => cell.Position == cellPosition)?.Number ?? -1;
        }

        public void UpdateAllMissingNumbers(IReadOnlyAvailableNumbers availableNumbers)
        {
            foreach (int number in availableNumbers.Missed)
            {
                foreach (var card in _cards)
                    if (card.UpdateMissingCells(number))
                        _lastMissedNumber = number;
            }
        }

        public void MarkAvailableNumbers(IReadOnlyAvailableNumbers availableNumbers, int amount)
        {
            if (amount > AvailableToMark(availableNumbers))
                throw new InvalidOperationException();

            int marksLeft = amount;

            foreach (int number in availableNumbers.Available)
            {
                foreach (var card in _cards)
                {
                    int availableInCard = card.AvailableCellsWithNumber(number);

                    int marksAmount = Mathf.Min(marksLeft, availableInCard);

                    marksLeft -= marksAmount;

                    card.MarkAvailableCells(number, marksAmount);

                    if (marksLeft == 0)
                        return;
                }
            }
        }

        public void RestoreMisses(int amount)
        {
            if (amount == 0)
                throw new ArgumentOutOfRangeException(nameof(amount));

            int missesAmount = MissesAmount;
            if (amount > missesAmount)
                throw new InvalidOperationException();

            int missesToRestore = Mathf.Min(amount, missesAmount);

            int missesRestored = 0;
            foreach (var cell in _cards.SelectMany(card => card.Cells).Where(cell => cell.Status == CellStatus.Missed))
            {
                cell.Restore();

                missesRestored += 1;
                if (missesRestored == missesToRestore)
                    break;
            }
        }

        public void ChangeCard(int cardIndex, ICard card)
        {
            _cards[cardIndex] = card;
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
