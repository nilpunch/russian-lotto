using UnityEngine;

namespace RussianLotto.Client
{
    public interface IBoard : IReadOnlyBoard
    {
        void Mark(int card, Vector2Int cellPosition);
        void Miss(int card, Vector2Int cellPosition);

        void UpdateAllMissingNumbers(IReadOnlyAvailableNumbers availableNumbers);
    }
}
