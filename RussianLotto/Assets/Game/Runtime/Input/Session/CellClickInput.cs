using System;
using RussianLotto.View;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RussianLotto.Input
{
    public class CellClickInput : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private CellView _cell;

        public event Action<Vector2Int> Clicked = delegate { };

        public void OnPointerClick(PointerEventData eventData)
        {
            Clicked.Invoke(_cell.CellPosition);
        }
    }
}
