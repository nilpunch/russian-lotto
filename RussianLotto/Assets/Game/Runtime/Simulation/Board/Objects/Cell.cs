using System;
using UnityEngine;

namespace RussianLotto.Client
{
    public class Cell : ICell
    {
        private CellStatus _status;

        public Cell(int number, Vector2Int position, CellStatus status)
        {
            _status = status;
            Number = number;
            Position = position;
        }

        public CellStatus Status => Number == 0 ? CellStatus.Zero : _status;
        public Vector2Int Position { get; }
        public int Number { get; }

        public void Mark()
        {
            if (Status != CellStatus.Available)
                throw new InvalidOperationException($"Cell is not available to {nameof(Mark)}.");

            _status = CellStatus.Marked;
        }

        public void Miss()
        {
            if (Status != CellStatus.Available)
                throw new InvalidOperationException($"Cell is not available to {nameof(Miss)}.");

            _status = CellStatus.Missed;
        }
    }
}
