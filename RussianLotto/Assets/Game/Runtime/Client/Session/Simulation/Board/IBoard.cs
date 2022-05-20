using UnityEngine;

namespace RussianLotto.Client
{
    public interface IBoard : IReadOnlyBoard
    {
        void Mark(int cardIndex, Vector2Int cellPosition);

        int GetNumberAt(int cardIndex, Vector2Int cellPosition);

        void UpdateAllMissingNumbers(IReadOnlyAvailableNumbers availableNumbers);

        void MarkAvailableNumbers(IReadOnlyAvailableNumbers availableNumbers, int amount);

        void RestoreMisses(int amount);

        void ChangeCard(int cardIndex, ICard card);
    }
}
