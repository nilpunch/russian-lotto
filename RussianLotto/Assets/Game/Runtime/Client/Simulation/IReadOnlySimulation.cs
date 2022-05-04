using RussianLotto.Networking;
using RussianLotto.View;

namespace RussianLotto.Client
{
    public interface IReadOnlySimulation : IVisualization<ISimulationView>
    {
        GameState GameState { get; }
    }
}
