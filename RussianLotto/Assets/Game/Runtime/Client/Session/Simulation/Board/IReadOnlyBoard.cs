﻿using System.Collections.Generic;
using RussianLotto.View;
using UnityEngine;

namespace RussianLotto.Client
{
    public interface IReadOnlyBoard : IVisualization<IBoardView>
    {
        int MissesAmount { get; }

        int AvailableToMark(IReadOnlyAvailableNumbers availableNumbers);

        IEnumerable<(int, Vector2Int)> AllAvailableToMarkCells(IReadOnlyAvailableNumbers availableNumbers);

        bool IsAvailable(int card, Vector2Int cellPosition);

        bool IsWin();
    }
}
