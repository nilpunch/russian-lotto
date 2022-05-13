using UnityEngine;

namespace RussianLotto.View
{
    public class SimulationView : MonoBehaviour, ISimulationView
    {
        [SerializeField] private BoardView _boardView;
        [SerializeField] private AvailableNumbersView _availableNumbersView;
        
        public IBoardView Board => _boardView;
        public IAvailableNumbersView AvailableNumbers => _availableNumbersView;
    }
}