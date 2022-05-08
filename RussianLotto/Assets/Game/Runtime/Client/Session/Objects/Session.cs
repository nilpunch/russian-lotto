using System;

namespace RussianLotto.Client
{
    public class Session : ISession
    {
        private LocalSimulation _localSimulation;

        public bool HasSimulation => _localSimulation != null;

        public ISimulation Simulation => HasSimulation ? throw new InvalidOperationException() : _localSimulation;

        public void GenerateSimulation(int seed, GameType gameType, bool shuffled)
        {
            if (HasSimulation)
                throw new InvalidOperationException();

            var roomRandom = new RandomNumberGenerator(seed);
            var numbersGenerator = new NumbersFactory(90, roomRandom);
            var numbers = numbersGenerator.Create();

            var userRandom = new RandomNumberGenerator(UnityEngine.Random.Range(int.MinValue, int.MaxValue));

            IFactory<ICard> cardFactory = gameType switch
            {
                GameType.Classic => new ClassicGameCardFactory(userRandom, shuffled),
                GameType.Fast => new FastGameCardFactory(userRandom, shuffled),
                _ => throw new InvalidOperationException()
            };

            var boardFactory = new BoardFactory(3, cardFactory);
            var board = boardFactory.Create();

            var availableNumbers = new AvailableNumbers(3, 5, numbers);
            _localSimulation = new LocalSimulation(board, availableNumbers);
        }

        public void DeleteSimulation()
        {
            if (!HasSimulation)
                throw new InvalidOperationException();

            _localSimulation = null;
        }
    }
}
