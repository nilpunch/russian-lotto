using RussianLotto.Client;
using RussianLotto.Input;
using UnityEngine;

namespace RussianLotto.Application
{
    public class InputRoot : MonoBehaviour, IInput, ILobbyInput
    {
        [SerializeField] private ButtonInput _connectToRandomRoom = null;
        [SerializeField] private ButtonInput _leaveRoom = null;

        [Space, SerializeField] private SwitchInput _shuffledSwitch = null;
        [SerializeField] private SwitchInput _gameTypeSwitch = null;

        public IButtonElement ConnectToRandomRoom => _connectToRandomRoom;
        public IButtonElement LeaveRoom => _leaveRoom;

        public ILobbyInput Lobby => this;

        public ISwitchElement<bool> ShuffledSwitch => _shuffledSwitch;
        public ISwitchElement<GameType> GameTypeSwitch => new BoolSwitchToGameTypeSwitchAdapter(_gameTypeSwitch);
    }
}
