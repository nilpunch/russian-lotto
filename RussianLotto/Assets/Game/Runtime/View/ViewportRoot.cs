using UnityEngine;

namespace RussianLotto.View
{
    public class ViewportRoot : MonoBehaviour, IViewport
    {
        [SerializeField] private SimulationView _simulationView;
        [SerializeField] private ScreensPresentation _screensPresentation;
        [SerializeField] private PlayersView _playersView;

        public IScreensPresentation ScreensPresentation => _screensPresentation;
        public ISimulationView SimulationView => _simulationView;
        public IPlayersView PlayersView => _playersView;
    }
}
