namespace RussianLotto.View
{


    public interface IViewport
    {
        public IPlayersView PlayersView { get; }
        public ISimulationView SimulationView { get; }
        public IScreensPresentation ScreensPresentation { get; }
    }
}
