using System.Collections.Generic;
using System.Text;
using RussianLotto.Networking;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RussianLotto.View
{
    public class PlayersView : MonoBehaviour, IPlayersView
    {
        [SerializeField] private TextMeshProUGUI _playersAmountText;
        [SerializeField] private TextMeshProUGUI _waitForPlayersLabel;
        [SerializeField] private TextMeshProUGUI _waitForGameStartLabel;
        [SerializeField] private Image _waitIcon;

        private StringBuilder _stringBuilder;

        private void Awake()
        {
            _stringBuilder = new StringBuilder();
        }

        public void DrawPlayers(IReadOnlyCollection<IPlayer> connectedPlayers, int maxPlayersAmount)
        {
            _stringBuilder.Clear();

            _stringBuilder.Append(connectedPlayers.Count).Append('/').Append(maxPlayersAmount);

            _playersAmountText.text = _stringBuilder.ToString();
        }

        public void ShowGameReadyToStart(bool roomHasMinPlayersAmount)
        {
            _waitForGameStartLabel.enabled = roomHasMinPlayersAmount;
            _waitForPlayersLabel.enabled = !roomHasMinPlayersAmount;
            _waitIcon.enabled = _waitForGameStartLabel.enabled;
        }
    }
}
