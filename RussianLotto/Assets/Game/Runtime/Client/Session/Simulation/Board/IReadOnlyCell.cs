using UnityEngine;

namespace RussianLotto.Client
{
    public interface IReadOnlyCell
    {
        CellStatus Status { get; }
        Vector2Int Position { get; }
        int Number { get; }

        bool IsAvailable => Status == CellStatus.Available;
    }
}
