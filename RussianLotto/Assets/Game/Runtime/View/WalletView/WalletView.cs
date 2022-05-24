using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace RussianLotto.View
{
    public class WalletView : MonoBehaviour, IWalletView
    {
        [SerializeField] private TextMeshProUGUI _moneyText;

        private static readonly List<(int, string)> _digitsPostfixes = new ()
        {
            (1, ""),
            (1000, "К"),
            (1000000, "М"),
        };

        private int _lastMoneys = -1;

        public void ShowMoneys(int amount)
        {
            if (_lastMoneys == amount)
                return;

            _lastMoneys = amount;
            _moneyText.text = FormatMoneys(amount);
        }

        public static string FormatMoneys(int amount)
        {
            for (int i = 0; i < _digitsPostfixes.Count - 1; ++i)
            {
                var (currentValue, currentPostfix) = _digitsPostfixes[i];
                var (nextValue, nextPosfix) = _digitsPostfixes[i+1];

                bool nextIsLast = i >= _digitsPostfixes.Count - 2;

                if (nextValue > amount)
                {
                    float truncedResult = (float)amount / currentValue;

                    return truncedResult.ToString(truncedResult >= 100 ? "0" : "0.0").Replace(".0", "") + currentPostfix;
                }

                if (nextIsLast)
                {
                    float truncedResult = (float)amount / nextValue;

                    return truncedResult.ToString(truncedResult >= 100 ? "0" : "0.0").Replace(".0", "") + nextPosfix;
                }
            }

            return String.Empty;
        }
    }
}
