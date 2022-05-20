namespace RussianLotto.Client
{
    public interface IReadOnlySession
    {
        bool HasSimulation { get; }
        IReadOnlySimulation ReadOnlySimulation { get; }
    }
}
