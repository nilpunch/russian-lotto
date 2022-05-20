using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RussianLotto.View
{
    public class NumberView : MonoBehaviour
    {
        [SerializeField] private Image _backgroundImage = null;
        [SerializeField] private TextMeshProUGUI _numberText = null;

        private int _lastNumber;

        public virtual void Show(int number)
        {
            _backgroundImage.enabled = true;
            _numberText.enabled = true;

            if (_lastNumber == number)
                return;

            _lastNumber = number;
            _numberText.text = number.ToString();
        }

        public virtual void Hide()
        {
            _backgroundImage.enabled = false;
            _numberText.enabled = false;
        }
    }
}
