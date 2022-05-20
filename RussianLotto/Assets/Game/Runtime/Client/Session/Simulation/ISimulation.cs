using UnityEngine;

namespace RussianLotto.Client
{
    public interface ISimulation : IReadOnlySimulation, IGameLoop
    {
        void StartGame();
        void FinishGame();

        void TryMarkCell(int card, Vector2Int cellPosition);

        void ChangeCardToNewOne(int cardIndex);
        void TopUpAutomaticMarkBonus();
        void TopUpHighlithAvailableBonus();
        void TopUpMarkMissesBonus();
    }
}
