using System.Collections.Generic;
using UnityEngine;

namespace RussianLotto.View
{
    public class AvailableNumbersView : MonoBehaviour, IAvailableNumbersView
    {
        [SerializeField] private NumberView[] _numbers = null;

        public void DrawAvailableNumbers(IReadOnlyList<int> numbers, int from, int amount)
        {
            for (int i = 0; i < amount; ++i)
            {
                int numberIndex = i + from;
                int viewIndex = amount - i - 1;

                if (numberIndex >= 0 && numberIndex < numbers.Count)
                    _numbers[viewIndex].Show(numbers[numberIndex]);
                else
                    _numbers[viewIndex].Hide();
            }
        }
    }
}
