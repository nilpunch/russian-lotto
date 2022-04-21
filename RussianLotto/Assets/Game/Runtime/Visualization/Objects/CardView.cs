using System;
using System.Collections.Generic;
using System.Linq;
using RussianLotto.Client;
using UnityEngine;

namespace RussianLotto.View
{
    public class CardView : MonoBehaviour
    {
        [SerializeField] private CellView[] _cells = null;

        [Space, SerializeField] private RectTransform _gridRect = null;
        [SerializeField] private CellView _cellPrefab = null;
        [SerializeField] private Vector2Int _gridSize = Vector2Int.zero;

        private void Awake()
        {
            foreach ((Vector2Int cellIndex, Vector2 position, Vector2 size) in CalculateCellsPositions())
            {
                CellView cellView = Instantiate(_cellPrefab, _gridRect);
                cellView.RectTransform.anchoredPosition = position;
                cellView.RectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, size.x);
                cellView.RectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size.y);
                cellView.CellPosition = cellIndex;
            }
        }

        public void DrawCells(IReadOnlyCollection<IReadOnlyCell> cells)
        {
            foreach (var cell in cells)
            {
                CellView cellView = _cells.First(view => view.CellPosition == cell.Position);
                cellView.SetNumber(cell.Number);
                cellView.SetStatus(cell.Status);
            }
        }

        private void OnDrawGizmosSelected()
        {
            if (_gridRect == null)
                return;

            Gizmos.matrix = _gridRect.localToWorldMatrix;
            Gizmos.color = Color.white;

            foreach ((Vector2Int _, Vector2 position, Vector2 size) in CalculateCellsPositions())
            {
                Gizmos.DrawWireCube(position, size);
            }
        }

        private IEnumerable<(Vector2Int cellIndex, Vector2 position, Vector2 size)> CalculateCellsPositions()
        {
            if (_gridRect == null)
                throw new NullReferenceException();

            if (_gridSize.x <= 0 || _gridSize.y <= 0)
                yield break;

            Rect gridRect = _gridRect.rect;

            Vector2 cellSize = new(gridRect.size.x / _gridSize.x, gridRect.size.y / _gridSize.y);
            float minCellSize = Mathf.Min(cellSize.x, cellSize.y);

            Vector2 fittingCellSize = new(minCellSize, minCellSize);

            Vector2 firstCellPadding = fittingCellSize / 2f;
            Vector2 centrationOffset = (_gridRect.rect.size - Vector2.Scale(fittingCellSize, _gridSize)) / 2f;
            Vector2 firstCellPosition = _gridRect.rect.min + firstCellPadding + centrationOffset;

            for (int x = 0; x < _gridSize.x; ++x)
            {
                for (int y = 0; y < _gridSize.y; ++y)
                {
                    Vector2 offset = new(fittingCellSize.x * x, fittingCellSize.y * y);;
                    Vector2 position = firstCellPosition + offset;
                    yield return (new Vector2Int(x, y), position, fittingCellSize);
                }
            }
        }
    }
}
