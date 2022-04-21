using UnityEngine;

namespace RussianLotto.Client
{
    public interface ICard : IReadOnlyCard
    {
        void Mark(Vector2Int cell);
        void Miss(Vector2Int cell);
    }
}
