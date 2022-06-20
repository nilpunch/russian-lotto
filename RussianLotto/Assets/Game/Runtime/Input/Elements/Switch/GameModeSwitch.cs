using System;
using DG.Tweening;
using RussianLotto.Client;
using UnityEngine;
using UnityEngine.UI;

namespace RussianLotto.Input
{
    public class GameModeSwitch : MonoBehaviour, ISwitchElement<GameType>
    {
        [SerializeField] private Button _button;
        [SerializeField] private RectTransform _moveingSwitch;
        [SerializeField] private float _movingSpeed;

        private Vector2 _startPosition;

        public GameType State { get; private set; }

        private void Awake()
        {
            State = GameType.Classic;
            _startPosition = _moveingSwitch.anchoredPosition;

            _button.onClick.AddListener(OnClicked);
        }

        private void OnClicked()
        {
            State = State switch
            {
                GameType.Classic => GameType.Fast,
                GameType.Fast => GameType.Classic,
                _ => throw new ArgumentOutOfRangeException(nameof(State))
            };

            MoveSwitch(State);
        }

        private void MoveSwitch(GameType position)
        {
            float destination = position == GameType.Classic ? _startPosition.x : -_startPosition.x;

            float moveTime = Mathf.Abs(_startPosition.x * 2f) / _movingSpeed;

            _moveingSwitch.DOKill();

            _moveingSwitch.DOAnchorPos(new Vector2(destination, _startPosition.y), moveTime).SetEase(Ease.InOutCubic);
        }

        public bool Active
        {
            get => _button.interactable;
            set
            {
                if (_button.interactable != value)
                    _button.interactable = value;
            }
        }
    }
}
