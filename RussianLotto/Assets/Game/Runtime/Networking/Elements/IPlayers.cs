using System.Collections.Generic;
using RussianLotto.Client;
using RussianLotto.View;

namespace RussianLotto.Networking
{
    public interface IPlayers : IVisualization<IPlayersView>
    {
        int MaxPlayersAmount { get; }
        int MinPlayersAmountToStart { get; }
        IReadOnlyCollection<IPlayer> ConnectedPlayers { get; }
    }
}
