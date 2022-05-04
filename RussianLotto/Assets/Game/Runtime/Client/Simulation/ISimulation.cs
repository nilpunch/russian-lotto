using RussianLotto.View;

namespace RussianLotto.Client
{
    public interface ISimulation : IReadOnlySimulation, IGameLoop
    {
        void StartGame();
    }
}
