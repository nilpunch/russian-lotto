using System;
using System.Collections;
using RussianLotto.View;
using UnityEngine;

namespace RussianLotto.Input
{
    public class CardCellClickInput : MonoBehaviour
    {
        [SerializeField] private CardView _card;

        public event Action<int, Vector2Int> Clicked = delegate { };

        private IEnumerator Start()
        {
            yield return new WaitUntil(() => _card.Initialized);

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
