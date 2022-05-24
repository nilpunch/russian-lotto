namespace RussianLotto.View
{
    public interface IViewport
    {
        public IPlayersView PlayersView { get; }
        public IWalletView WalletView { get; }
        public ISimulationView SimulationView { get; }
        public IWinOrLoseView WinOrLoseView { get; }
        public IScreensPresentation ScreensPresentation { get; }
    }
}
