using UnityEngine;

namespace RussianLotto.Input
{
    public class InputRoot : MonoBehaviour, IInput
    {
        [SerializeField] private MainMenuInput _mainMenuInput;
        [SerializeField] private SessionInput _sessionInput;

        public IMainMenuInput MainMenu => _mainMenuInput;
        public ISessionInput Session => _sessionInput;
    }
}
