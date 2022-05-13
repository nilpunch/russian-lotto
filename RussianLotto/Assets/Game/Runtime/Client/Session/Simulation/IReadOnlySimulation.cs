using RussianLotto.View;

namespace RussianLotto.Client
{
    public interface IReadOnlySimulation : IVisualization<ISimulationView>
    {
        SimulationState State { get; }
    }
}
