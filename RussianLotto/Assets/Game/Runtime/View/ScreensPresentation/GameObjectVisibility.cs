using System;

namespace RussianLotto.View
{
    public class GameObjectVisibility : VisibilityElement
    {
        public override void Show()
        {
            gameObject.SetActive(true);
        }

        public override void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
