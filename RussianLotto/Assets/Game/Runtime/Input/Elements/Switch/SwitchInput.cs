using UnityEngine.UI;

namespace RussianLotto.Input
{
    public class SwitchInput : Toggle, ISwitchElement<bool>
    {
        public bool State => isOn;

        public bool Active
        {
            get => interactable;
            set
            {
                if (interactable != value)
                    interactable = value;
            }
        }
    }
}
