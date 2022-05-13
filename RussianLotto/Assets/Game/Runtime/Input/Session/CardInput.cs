using System;
using RussianLotto.View;
using UnityEngine;

namespace RussianLotto.Input
{
    public class CardInput : MonoBehaviour
    {
        [SerializeField] private CardView _card;

        public event Action<int, Vector2Int> Clicked = delegate { };

        private void Start()
        {
            foreach (var cellClickHandler in _card.Cells)
                cellClickHandler.GetComponent<CellClickInput>().Clicked += OnCellClicked;
        }

        private void OnDestroy()
        {
            foreach (var cellClickHandler in _card.Cells)
                cellClickHandler.GetComponent<CellClickInput>().Clicked -= OnCellClicked;
        }

        private void OnCellClicked(Vector2Int cellPosition)
        {
            Clicked.Invoke(_card.CardIndex, cellPosition);
        }
    }
}
