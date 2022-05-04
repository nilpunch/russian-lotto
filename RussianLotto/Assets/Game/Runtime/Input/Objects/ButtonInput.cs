using System;
using UnityEngine.UI;

namespace RussianLotto.Input
{
    public class ButtonInput : Button, IButtonElement
    {
        public bool Active
        {
            get => interactable;
            set
            {
                if (interactable != value)
                    interactable = value;
            }
        }

        public bool Pressed { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            onClick.AddListener(Press);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            onClick.RemoveListener(Press);
        }

        public void Release()
        {
            if (Pressed == false)
                throw new InvalidOperationException($"Trying to {nameof(Release)} a {nameof(ButtonInput)} while not {nameof(Release)}.");

            Pressed = false;
        }

        private void Press()
        {
            Pressed = true;
        }
    }
}
