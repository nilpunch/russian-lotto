using UnityEngine;

namespace RussianLotto.View
{
    public class ViewportRoot : MonoBehaviour, IViewport
    {
        [SerializeField] private SimulationView _simulationView;
        [SerializeField] private ScreensPresentation _screensPresentation;

        public IPresentation Presentation => _screensPresentation;
        public ISimulationView SimulationView => _simulationView;
    }
}
