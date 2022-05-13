using RussianLotto.Client;
using RussianLotto.Input;
using UnityEngine;

namespace RussianLotto.Input
{
    public class MainMenuInput : MonoBehaviour, IMainMenuInput
    {
        [SerializeField] private ButtonInput _connectToRandomRoom;
        [SerializeField] private ButtonInput _leaveRoom;
        [SerializeField] private SwitchInput _shuffledSwitch;
        [SerializeField] private SwitchInput _gameTypeSwitch;

        public ISwitchElement<bool> ShuffledSwitch => _shuffledSwitch;
        public ISwitchElement<GameType> GameTypeSwitch => new BoolSwitchToGameTypeSwitchAdapter(_gameTypeSwitch);

        public IButtonElement ConnectToRandomRoom => _connectToRandomRoom;
        public IButtonElement LeaveRoom => _leaveRoom;
    }
}
