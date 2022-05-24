using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RussianLotto.View
{
    public class WinOrLoseView : MonoBehaviour, IWinOrLoseView
    {
        [SerializeField] private TextMeshProUGUI _loseBanner;
        [SerializeField] private TextMeshProUGUI _loseText;
        [SerializeField] private TextMeshProUGUI _winBanner;
        [SerializeField] private TextMeshProUGUI _winAmountText;
        [SerializeField] private Image _winMoneyIcon;

        public void ShowWin(int bank)
        {
            _loseBanner.enabled = false;
            _loseText.enabled = false;
            _winBanner.enabled = true;
            _winAmountText.enabled = true;
            _winMoneyIcon.enabled = true;

            _winAmountText.text = "+" + WalletView.FormatMoneys(bank);
        }

        public void ShowLose()
        {
            _loseBanner.enabled = true;
            _loseText.enabled = true;
            _winBanner.enabled = false;
            _winAmountText.enabled = false;
            _winMoneyIcon.enabled = false;
        }
    }
}
