using System.Collections.Generic;
using UnityEngine;

namespace RussianLotto.Client
{
    public interface IReadOnlyCard
    {
        IReadOnlyCollection<IReadOnlyCell> Cells { get; }

        bool IsAvailable(Vector2Int cell);
    }
}
