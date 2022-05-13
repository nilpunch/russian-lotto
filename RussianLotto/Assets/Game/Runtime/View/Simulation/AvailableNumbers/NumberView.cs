using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RussianLotto.View
{
    public class NumberView : MonoBehaviour
    {
        [SerializeField] private Image _backgroundImage = null;
        [SerializeField] private TextMeshProUGUI _numberText = null;

        public virtual void Show(int kegValue)
        {
            _backgroundImage.enabled = true;
            _numberText.enabled = true;
            _numberText.text = kegValue.ToString();
        }

        public virtual void Hide()
        {
            _backgroundImage.enabled = false;
            _numberText.enabled = false;
        }
    }
}
