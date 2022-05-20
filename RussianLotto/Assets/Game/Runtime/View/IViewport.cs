namespace RussianLotto.View
{
    public interface IViewport
    {
        public ISimulationView SimulationView { get; }
        public IScreensPresentation ScreensPresentation { get; }
    }
}
