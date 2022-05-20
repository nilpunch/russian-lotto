using System;
using UnityEngine;

namespace RussianLotto.View
{
    public class ScreenRoot : VisibilityElement
    {
        [SerializeField] private VisibilityElement[] _elements;

        [field: SerializeField] public Screen Screen { get; private set; }


        public override void Show()
        {
            foreach (VisibilityElement visibilityElement in _elements)
                visibilityElement.Show();
        }

        public override void Hide()
        {
            foreach (VisibilityElement visibilityElement in _elements)
                visibilityElement.Hide();
        }
    }
}
