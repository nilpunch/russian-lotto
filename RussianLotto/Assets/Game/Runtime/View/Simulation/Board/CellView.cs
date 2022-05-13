using System;
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
        [field: SerializeField] public Vector2Int CellPosition { get; set; } = Vector2Int.zero;
        [field: SerializeField] public RectTransform RectTransform { get; private set; } = null;

        private void Awake()
        {
            SetStatus(CellStatus.Zero);
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
                    _markImage.enabled = true;
                    _numberText.enabled = true;
                    break;
                case CellStatus.Missed:
                    _missImage.enabled = true;
                    _markImage.enabled = false;
                    _numberText.enabled = true;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(cellStatus), cellStatus, null);
            }
        }

        public void SetNumber(int number)
        {
            _numberText.text = number.ToString();
        }
    }
}
