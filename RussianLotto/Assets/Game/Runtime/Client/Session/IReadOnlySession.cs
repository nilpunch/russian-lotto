using RussianLotto.View;

namespace RussianLotto.Client
{
    public interface IReadOnlySession : IVisualization<IWalletView>, IVisualization<ISimulationView>, IVisualization<IWinOrLoseView>, IVisualization<IHighlightedCellsView>
    {
        bool HasSimulation { get; }
        IReadOnlySimulation ReadOnlySimulation { get; }
    }
}
