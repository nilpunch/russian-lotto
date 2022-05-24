using UnityEngine;
using UnityEngine.UI;

namespace RussianLotto.View
{
    [RequireComponent(typeof(Image))]
    public class ImageVisibility : VisibilityElement
    {
        private Image _image;

        private void Awake()
        {
            _image = GetComponent<Image>();
        }

        public override void Show()
        {
            _image.enabled = true;
        }

        public override void Hide()
        {
            _image.enabled = false;
        }
    }
}
