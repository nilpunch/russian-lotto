using RussianLotto.Client;

namespace RussianLotto.Input
{
    public interface IMainMenuInput
    {
        ISwitchElement<bool> ShuffledSwitch { get; }
        ISwitchElement<GameType> GameTypeSwitch { get; }

        public IButtonElement ConnectToRandomRoom { get; }
        public IButtonElement LeaveRoom { get; }
    }
}