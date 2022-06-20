namespace RussianLotto.Client
{
    public interface ISession : IReadOnlySession
    {
        ISimulation Simulation { get; }

        void GenerateSimulation(int seed, GameType gameType, bool shuffled);
        void DeleteSimulation();

        void Save();

        void TopUpAutomaticMarkBonus();
        void TopUpHighlithAvailableBonus();
        void TopUpMarkMissesBonus();

        bool CanBet(int amount);
        void Bet(int amount);
        void MultiplyBet(int multiplier);
        void Lose();
        void Win();
        void TryChangeCardToNewOne(int card);
    }
}
