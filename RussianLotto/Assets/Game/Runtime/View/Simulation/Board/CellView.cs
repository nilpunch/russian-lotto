using System;
using RussianLotto.Client;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RussianLotto.View
{
    public class CellView : MonoBehaviour
    {
        [SerializeField] private Image _missImage;
        [SerializeField] private Image _markImage;
        [SerializeField] private TextMeshProUGUI _numberText;
        [SerializeField] private TextMeshProUGUI _markNumberText;
        [SerializeField] private Image _highlightImage;
        [field: SerializeField] public Vector2Int CellPosition { get; set; } = Vector2Int.zero;
        [field: SerializeField] public RectTransform RectTransform { get; private set; } = null;

        private int _lastNumber;

        private void Awake()
        {
            SetStatus(CellStatus.Zero);
            Highlight(false);
        }

        public void Highlight(bool isHighlighted)
        {
            _highlightImage.enabled = isHighlighted;
        }

        public void SetStatus(CellStatus cellStatus)
        {
            switch (cellStatus)
            {
                case CellStatus.Zero:
                    _missImage.enabled = false;
                    _markImage.enabled = false;
                    _numberText.enabled = false;
                    break;
                case CellStatus.Available:
                    _missImage.enabled = false;
                    _markImage.enabled = false;
                    _numberText.enabled = true;
                    break;
                case CellStatus.Marked:
                    _missImage.enabled = false;
                    _numberText.enabled = false;
                    _markImage.enabled = true;
                    break;
                case CellStatus.Missed:
                    _missImage.enabled = true;
                    _markImage.enabled = false;
                    _numberText.enabled = true;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(cellStatus), cellStatus, null);
            }

            _markNumberText.enabled = _markImage.enabled;
        }

        public void SetNumber(int number)
        {
            if (_lastNumber == number)
                return;

            _lastNumber = number;
            _numberText.text = number.ToString();
            _markNumberText.text = _numberText.text;
        }
    }
}
