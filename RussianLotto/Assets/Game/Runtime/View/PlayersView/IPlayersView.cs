using System.Collections.Generic;
using RussianLotto.Networking;

namespace RussianLotto.View
{
    public interface IPlayersView
    {
        public void DrawPlayers(IReadOnlyCollection<IPlayer> connectedPlayers, int maxPlayersAmount);
        public void ShowGameReadyToStart(bool roomHasMinPlayersAmount);
    }
}
