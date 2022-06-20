using RussianLotto.View;

namespace RussianLotto.Client
{
    public interface IReadOnlySimulation : IVisualization<ISimulationView>, IVisualization<IHighlightedCellsView>
    {
        SimulationState State { get; }
        bool IsPlayerWin { get; }
    }
}
