using System.Collections.Generic;
using RussianLotto.Client;

namespace RussianLotto.Networking
{
    public interface IReadOnlyRoom : IPlayers
    {
        bool IsEntered { get; }
        bool IsOpenToJoin { get; }
        GameType GameType { get; }
        bool ShuffledMode { get; }
        bool CanSendCommands { get; }
    }
}
