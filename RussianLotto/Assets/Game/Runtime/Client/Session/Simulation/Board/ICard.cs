using System.Collections.Generic;
using UnityEngine;

namespace RussianLotto.Client
{
    public interface IReadOnlyCard
    {
        IReadOnlyCollection<IReadOnlyCell> ReadOnlyCells { get; }
        bool IsAvailable(Vector2Int cellPosition);
        int AvailableCellsWithNumber(int number);
        IEnumerable<Vector2Int> AllAvailableCellsWithNumber(int number);
        bool IsComplete();
    }

    public interface ICard : IReadOnlyCard
    {
        IReadOnlyCollection<ICell> Cells { get; }
        void Mark(Vector2Int cellPosition);
        bool UpdateMissingCells(int number);
        void MarkAvailableCells(int number, int amount);
    }
}
