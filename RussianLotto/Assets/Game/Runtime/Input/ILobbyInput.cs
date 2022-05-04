using RussianLotto.Client;

namespace RussianLotto.Input
{
    public interface ILobbyInput
    {
        ISwitchElement<bool> ShuffledSwitch { get; }
        ISwitchElement<GameType> GameTypeSwitch { get; }
    }
}
