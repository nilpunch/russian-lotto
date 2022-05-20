using System.Collections.Generic;
using UnityEngine;

namespace RussianLotto.Client
{
    public interface IReadOnlyCard
    {
        IReadOnlyCollection<IReadOnlyCell> ReadOnlyCells { get; }

        bool IsAvailable(Vector2Int cellPosition);

        int AvailableCellsWithNumber(int number);

        bool IsComplete();
    }
}
