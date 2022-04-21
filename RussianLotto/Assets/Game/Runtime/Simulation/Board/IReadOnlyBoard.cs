using System.Collections.Generic;
using RussianLotto.View;
using UnityEngine;

namespace RussianLotto.Client
{
    public interface IReadOnlyBoard : IVisualization<IBoardView>
    {
        bool IsAvailable(int card, Vector2Int cellPosition);
    }
}
