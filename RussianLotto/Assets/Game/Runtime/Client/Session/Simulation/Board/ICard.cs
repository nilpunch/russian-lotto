using System.Collections.Generic;
using UnityEngine;

namespace RussianLotto.Client
{
    public interface ICard : IReadOnlyCard
    {
        IReadOnlyCollection<ICell> Cells { get; }

        void Mark(Vector2Int cellPosition);

        bool UpdateMissingCells(int number);

        void MarkAvailableCells(int number, int amount);
    }
}
