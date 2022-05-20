using UnityEditor;
using UnityEngine;

namespace RussianLotto.View
{
    public static class RectTransformTools
    {
#if UNITY_EDITOR
        [MenuItem("CONTEXT/RectTransform/Pin Anchors Around")]
        public static void PinAnchorsAround(MenuCommand menuCommand)
        {
            RectTransform transform = (RectTransform)menuCommand.context;

            Undo.RecordObject(transform, nameof(PinAnchorsAround));

            var parentTransform = transform.parent.GetComponent<RectTransform>();

            Rect containerRect = parentTransform.rect;
            Rect rect = transform.rect;

            Vector2 normalizedMaxCorner = new Vector2((transform.anchoredPosition.x + containerRect.size.x / 2f) / containerRect.size.x,
                (transform.anchoredPosition.y + containerRect.size.y / 2f) / containerRect.size.y) +
                Vector2.Scale(rect.size / 2f, new Vector2(1 / containerRect.size.x, 1 / containerRect.size.y));

            Vector2 normalizedMinCorner = new Vector2((transform.anchoredPosition.x + containerRect.size.x / 2f) / containerRect.size.x,
                                              (transform.anchoredPosition.y + containerRect.size.y / 2f) / containerRect.size.y) -
                                          Vector2.Scale(rect.size / 2f, new Vector2(1 / containerRect.size.x, 1 / containerRect.size.y));

            transform.anchorMin = normalizedMinCorner;
            transform.anchorMax = normalizedMaxCorner;

            transform.offsetMax = Vector2.zero;
            transform.offsetMin = Vector2.zero;

            EditorUtility.SetDirty(transform);
        }

        [MenuItem("CONTEXT/RectTransform/Pin to X")]
        public static void PinAnchorToX(MenuCommand menuCommand)
        {
            RectTransform transform = (RectTransform)menuCommand.context;

            Undo.RecordObject(transform, nameof(PinAnchorToX));

            var parentTransform = transform.parent.GetComponent<RectTransform>();

            Rect containerRect = parentTransform.rect;

            Vector2 normalizedPosition = new Vector2((transform.anchoredPosition.x + containerRect.size.x / 2f) / containerRect.size.x,
                (transform.anchoredPosition.y + containerRect.size.y / 2f) / containerRect.size.y);

            transform.anchorMin = new Vector2(normalizedPosition.x, transform.anchorMin.y);
            transform.anchorMax = new Vector2(normalizedPosition.x, transform.anchorMax.y);
            transform.anchoredPosition *= Vector2.up;

            EditorUtility.SetDirty(transform);
        }

        [MenuItem("CONTEXT/RectTransform/Pin to Y")]
        public static void PinAnchorToY(MenuCommand menuCommand)
        {

            RectTransform transform = (RectTransform)menuCommand.context;

            Undo.RecordObject(transform, nameof(PinAnchorToY));

            var parentTransform = transform.parent.GetComponent<RectTransform>();

            Rect containerRect = parentTransform.rect;

            Vector2 normalizedPosition = new Vector2((transform.anchoredPosition.x + containerRect.size.x / 2f) / containerRect.size.x,
                (transform.anchoredPosition.y + containerRect.size.y / 2f) / containerRect.size.y);

            transform.anchorMin = new Vector2(transform.anchorMin.x, normalizedPosition.y);
            transform.anchorMax = new Vector2(transform.anchorMax.x, normalizedPosition.y);
            transform.anchoredPosition *= Vector2.right;

            EditorUtility.SetDirty(transform);
        }
#endif
    }
}
