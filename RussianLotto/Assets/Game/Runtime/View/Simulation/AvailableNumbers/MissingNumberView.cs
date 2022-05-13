using UnityEngine;
using UnityEngine.UI;

namespace RussianLotto.View
{
    public class MissingNumberView : NumberView
    {
        [SerializeField] private Image _redCrossImage = null;

        public override void Show(int kegValue)
        {
            base.Show(kegValue);
            _redCrossImage.enabled = true;
        }

        public override void Hide()
        {
            base.Hide();
            _redCrossImage.enabled = false;
        }
    }
}
