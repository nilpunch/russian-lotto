#if UNITY_EDITOR
using UnityEditor;
#endif

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using RussianLotto.Client;
using UnityEngine;
using UnityEngine.UI;

namespace RussianLotto.View
{
    public class CardView : MonoBehaviour
    {
        [Space, SerializeField] private RectTransform _gridRect;
        [SerializeField] private RectTransform _cellsParent;
        [SerializeField] private CellView _cellPrefab;
        [SerializeField] private Vector2Int _gridSize = Vector2Int.zero;

        [Space, SerializeField] private RectTransform _controllableRect;
        [SerializeField, Range(0f, 1f)] private float _offset = 0.1f;

        public int CardIndex { get; set; }
        public List<CellView> Cells { get; private set; }

        public bool Initialized { get; private set; }

        private IEnumerator Start()
        {
            yield return null;

            Initialized = true;

            Cells = new List<CellView>();
            foreach ((Vector2Int cellIndex, Vector2 position, Vector2 size) in CalculateCellsPositions())
            {
                CellView cellView = Instantiate(_cellPrefab, _cellsParent);
                cellView.RectTransform.anchorMax = Vector2.one * 0.5f;
                cellView.RectTransform.anchorMin = Vector2.one * 0.5f;
                cellView.RectTransform.anchoredPosition = position;
                cellView.RectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, size.x);
                cellView.RectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size.y);
                cellView.CellPosition = cellIndex;
                Cells.Add(cellView);
            }
        }

#if UNITY_EDITOR
        [ContextMenu("Fix Position")]
        private void FixPosition()
        {
            if (_controllableRect == null)
                return;

            Rect rect = CalculateFitRect();

            Undo.RecordObject(_controllableRect, "FixControllableRect");

            _controllableRect.anchorMax = Vector2.one * 0.5f;
            _controllableRect.anchorMin = Vector2.one * 0.5f;
            _controllableRect.localPosition = rect.center;
            _controllableRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, rect.size.x);
            _controllableRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, rect.size.y);
            EditorUtility.SetDirty(_controllableRect);
        }
        #endif

        public void DrawCells(IReadOnlyCollection<IReadOnlyCell> cells)
        {
            foreach (var cellView in Cells)
            {
                cellView.SetStatus(CellStatus.Zero);
            }

            foreach (var cell in cells)
            {
                CellView cellView = Cells.First(view => view.CellPosition == cell.Position);
                cellView.SetNumber(cell.Number);
                cellView.SetStatus(cell.Status);
            }
        }

        private void OnDrawGizmos()
        {
            if (_gridRect == null)
                return;

            Gizmos.matrix = _gridRect.localToWorldMatrix;
            Gizmos.color = Color.blue;

            foreach ((Vector2Int _, Vector2 position, Vector2 size) in CalculateCellsPositions())
                Gizmos.DrawWireCube(position, size);
        }

        private IEnumerable<(Vector2Int cellIndex, Vector2 position, Vector2 size)> CalculateCellsPositions()
        {
            if (_gridRect == null)
                throw new NullReferenceException();

            if (_gridSize.x <= 0 || _gridSize.y <= 0)
                yield break;

            Rect originalRect = _gridRect.rect;

            Rect gridRectWithOffset = originalRect;
            gridRectWithOffset.size *= _offset;

            Vector2 cellSize = new(gridRectWithOffset.size.x / _gridSize.x, gridRectWithOffset.size.y / _gridSize.y);
            float minCellSize = Mathf.Min(cellSize.x, cellSize.y);

            Vector2 fittingCellSize = new(minCellSize, minCellSize);

            Vector2 firstCellPadding = fittingCellSize / 2f;
            Vector2 centrationOffset = (originalRect.size - Vector2.Scale(fittingCellSize, _gridSize)) / 2f;
            Vector2 firstCellPosition = originalRect.min + firstCellPadding + centrationOffset;

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

        private Rect CalculateFitRect()
        {
            Rect gridRect = _gridRect.rect;

            gridRect.size *= _offset;

            if (_gridSize.x <= 0 || _gridSize.y <= 0)
                return gridRect;

            Rect originalRect = _gridRect.rect;

            Rect gridRectWithOffset = originalRect;
            gridRectWithOffset.size *= _offset;

            Vector2 cellSize = new(gridRectWithOffset.size.x / _gridSize.x, gridRectWithOffset.size.y / _gridSize.y);
            float minCellSize = Mathf.Min(cellSize.x, cellSize.y);

            Vector2 fittingCellSize = new(minCellSize, minCellSize);

            Vector2 centrationOffset = (originalRect.size - Vector2.Scale(fittingCellSize, _gridSize)) / 2f;

            return new Rect(gridRect.position + centrationOffset, new Vector2(fittingCellSize.x * _gridSize.x, fittingCellSize.y * _gridSize.y));
        }
    }
}
