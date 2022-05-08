using UnityEngine;

namespace RussianLotto.Client
{
    public interface ICard : IReadOnlyCard
    {
        void Mark(Vector2Int cellPosition);
        void Miss(Vector2Int cellPosition);

        void UpdateMissingCells(int number);
    }
}
