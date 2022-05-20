using RussianLotto.Client;
using UnityEngine;

namespace RussianLotto.Input
{
    public class MainMenuInput : MonoBehaviour, IMainMenuInput
    {
        [SerializeField] private ButtonInput _connectToRandomRoom;
        [SerializeField] private ButtonInput _leaveRoom;
        [SerializeField] private SwitchInput _shuffledSwitch;
        [SerializeField] private GameModeSwitch _gameTypeSwitch;

        public ISwitchElement<bool> ShuffledSwitch => _shuffledSwitch;
        public ISwitchElement<GameType> GameTypeSwitch => _gameTypeSwitch;

        public IButtonElement ConnectToRandomRoom => _connectToRandomRoom;
        public IButtonElement LeaveRoom => _leaveRoom;
    }
}
