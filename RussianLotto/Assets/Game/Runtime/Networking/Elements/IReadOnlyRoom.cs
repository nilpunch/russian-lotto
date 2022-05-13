using System.Collections.Generic;
using RussianLotto.Client;

namespace RussianLotto.Networking
{
    public interface IReadOnlyRoom
    {
        bool IsEntered { get; }
        bool IsOpenToJoin { get; }
        int MaxPlayersAmount { get; }
        GameType GameType { get; }
        bool ShuffledMode { get; }
        IReadOnlyCollection<IPlayer> ConnectedPlayers { get; }
        bool CanSendCommands { get; }
    }
}
