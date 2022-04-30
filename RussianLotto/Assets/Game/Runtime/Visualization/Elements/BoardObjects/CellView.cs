using UnityEngine;

namespace RussianLotto.View
{
    public class CellView : MonoBehaviour
    {
        [field: SerializeField] public Vector2Int CellPosition { get; set; } = Vector2Int.zero;
        [field: SerializeField] public RectTransform RectTransform { get; private set; } = null;

        public void SetStatus(CellStatus cellStatus)
        {

        }

        public void SetNumber(int number)
        {

        }
    }
}
