using UnityEngine;

namespace RussianLotto.View
{
    [RequireComponent(typeof(Canvas))]
    public class CanvasVisibility : VisibilityElement
    {
        private Canvas _canvas;

        private void Awake()
        {
            _canvas = GetComponent<Canvas>();
        }

        public override void Show()
        {
            _canvas.enabled = true;
        }

        public override void Hide()
        {
            _canvas.enabled = false;
        }
    }
}
