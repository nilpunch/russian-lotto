using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RussianLotto.Client
{
    public class Card : ICard
    {
        private IReadOnlyCollection<ICell> _cells;

        public Card(IReadOnlyCollection<ICell> cells)
        {
            _cells = cells;
        }

        public IReadOnlyCollection<IReadOnlyCell> ReadOnlyCells => _cells;

        public IReadOnlyCollection<ICell> Cells => _cells;

        public bool IsAvailable(Vector2Int cellPosition)
        {
            return _cells.Any(cell => cell.Position == cellPosition && cell.IsAvailable);
        }

        public int AvailableCellsWithNumber(int number)
        {
            return _cells.Count(cell => cell.Number == number && cell.IsAvailable);
        }

        public bool IsComplete()
        {
            return _cells.All(cell => cell.Status == CellStatus.Marked);
        }

        public void Mark(Vector2Int cellPosition)
        {
            if (!IsAvailable(cellPosition))
                throw new InvalidOperationException();

            _cells.Where(cell => cell.Position == cellPosition && cell.IsAvailable).FirstOrDefault()?.Mark();
        }

        public void Miss(Vector2Int cellPosition)
        {
            if (!IsAvailable(cellPosition))
                throw new InvalidOperationException();

            _cells.Where(cell => cell.Position == cellPosition && cell.IsAvailable).FirstOrDefault()?.Miss();
        }

        public bool UpdateMissingCells(int number)
        {
            bool missCell = false;

            foreach (var cell in _cells.Where(cell => cell.Number == number && cell.IsAvailable))
            {
                missCell = true;
                cell.Miss();
            }

            return missCell;
        }

        public void MarkAvailableCells(int number, int amount)
        {
            if (amount > AvailableCellsWithNumber(number))
                throw new InvalidOperationException();

            int markCount = 0;

            foreach (var cell in _cells.Where(cell => cell.Number == number && cell.IsAvailable))
            {
                cell.Mark();

                markCount += 1;
                if (markCount == amount)
                    break;
            }
        }
    }
}
