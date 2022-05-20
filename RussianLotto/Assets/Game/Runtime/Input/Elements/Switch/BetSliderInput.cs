using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RussianLotto.Input
{
    public class BetSliderInput : Slider, ISwitchElement<int>
    {
        [SerializeField] private TextMeshProUGUI _valueText = null;

        private readonly Dictionary<int, int> _bets = new()
        {
            {0, 100},
            {1, 200},
            {2, 500},
            {3, 1000},
            {4, 2000},
            {5, 5000},
            {6, 10000},
            {7, 50000},
            {8, 100000},
            {9, 500000},
            {10, 1000000},
        };

        public int State => _bets[(int)value];

        protected override void Awake()
        {
            base.Awake();

            onValueChanged.AddListener(OnValueChanged);

            _valueText.text = State.ToString();
        }

        private void OnValueChanged(float value)
        {
            _valueText.text = State.ToString();
        }

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
