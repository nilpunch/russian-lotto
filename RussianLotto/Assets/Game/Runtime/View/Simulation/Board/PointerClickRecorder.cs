using UnityEngine;
using UnityEngine.EventSystems;

namespace RussianLotto.View
{
    public class PointerClickRecorder : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private CellView _cell;

        public void OnPointerClick(PointerEventData eventData)
        {

        }
    }
}
