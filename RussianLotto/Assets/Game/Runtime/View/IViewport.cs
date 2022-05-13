namespace RussianLotto.View
{
    public interface IViewport
    {
        public ISimulationView SimulationView { get; }
        public IPresentation Presentation { get; }
    }
}
