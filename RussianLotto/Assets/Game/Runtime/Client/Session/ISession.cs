namespace RussianLotto.Client
{
    public interface ISession : IReadOnlySession
    {
        ISimulation Simulation { get; }
        void GenerateSimulation(int seed, GameType gameType, bool shuffled);
        void DeleteSimulation();
    }
}
