using UnityEngine;

namespace RussianLotto.Client
{
    public interface IBoard : IReadOnlyBoard
    {
        void Mark(int card, Vector2Int cellPosition);

        int GetNumberAt(int cardIndex, Vector2Int cellPosition);

        void UpdateAllMissingNumbers(IReadOnlyAvailableNumbers availableNumbers);
    }
}
