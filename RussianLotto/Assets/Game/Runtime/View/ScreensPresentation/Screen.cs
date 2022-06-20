using System;

namespace RussianLotto.View
{
    [Flags]
    public enum Screen
    {
        MainMenu = 1 << 0,
        Room = 1 << 1,
        Preparation = 1 << 2,
        Game = 1 << 3,
        Results = 1 << 4,
    }
}
