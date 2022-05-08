using System;
using UnityEngine;

namespace RussianLotto.Client
{
    public class Cell : ICell
    {
        public Cell(int number, Vector2Int position, CellStatus status)
        {
            if (number <= 0)
                throw new InvalidOperationException();

            if (status == CellStatus.Zero)
                throw new InvalidOperationException();

            Status = status;
            Number = number;
            Position = position;
        }

        public CellStatus Status { get; private set; }

        public Vector2Int Position { get; }
        public int Number { get; }

        public void Mark()
        {
            if (Status != CellStatus.Available)
                throw new InvalidOperationException($"Cell is not available to {nameof(Mark)}.");

            Status = CellStatus.Marked;
        }

        public void Miss()
        {
            if (Status != CellStatus.Available)
                throw new InvalidOperationException($"Cell is not available to {nameof(Miss)}.");

            Status = CellStatus.Missed;
        }
    }
}
