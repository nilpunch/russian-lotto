using System;
using RussianLotto.Save;
using RussianLotto.View;

namespace RussianLotto.Client
{
    public class Session : ISession
    {
        private readonly IGameSaves _gameSaves;
        private readonly IBet _bet;
        private readonly IBonuses _bonuses;

        private ISimulation _localSimulation;
        private readonly IWallet _wallet;

        public bool HasSimulation => _localSimulation != null;

        public IReadOnlySimulation ReadOnlySimulation => Simulation;

        public ISimulation Simulation => HasSimulation ? _localSimulation : throw new InvalidOperationException();

        public Session(IOfflineMoneyEarn offlineMoneyEarn, IGameSaves gameSaves)
        {
            _wallet = gameSaves.WalletSave.Load();
            _bonuses = gameSaves.BonusesSave.Load();

            if (offlineMoneyEarn.HasEarn)
                offlineMoneyEarn.Collect(_wallet);

            _gameSaves = gameSaves;
            _bet = new Bet();
        }

        public void GenerateSimulation(int seed, GameType gameType, bool shuffled)
        {
            if (HasSimulation)
                throw new InvalidOperationException();

            var roomRandom = new RandomNumberGenerator(seed);

            IFactory<INumbers> numbersFactory = gameType switch
            {
                GameType.Classic => new NumbersFactory(90, roomRandom),
                GameType.Fast => new NumbersFactory(36, roomRandom),
                _ => throw new InvalidOperationException()
            };

            var numbers = numbersFactory.Create();

            var userRandom = new RandomNumberGenerator(UnityEngine.Random.Range(int.MinValue, int.MaxValue));

            IFactory<ICard> cardFactory = gameType switch
            {
                GameType.Classic => new ClassicGameCardFactory(userRandom, shuffled),
                GameType.Fast => new FastGameCardFactory(userRandom, shuffled),
                _ => throw new InvalidOperationException()
            };

            var boardFactory = new BoardFactory(3, cardFactory);
            var board = boardFactory.Create();

            var availableNumbers = new AvailableNumbers(3000, 5, numbers);
            _localSimulation = new LocalSimulation(_bonuses, board, cardFactory, availableNumbers);
        }

        public void DeleteSimulation()
        {
            if (!HasSimulation)
                throw new InvalidOperationException();

            _localSimulation = null;
        }

        public void Save()
        {
            _gameSaves.BonusesSave.Save(_bonuses);
            _gameSaves.WalletSave.Save(_wallet);
        }

        public bool CanBet(int amount)
        {
            return _wallet.CanSpend(amount);
        }

        public void Bet(int amount)
        {
            _wallet.Spend(amount);
            _bet.Add(amount);
        }

        public void MultiplyBet(int multiplier)
        {
            _bet.Multiply(multiplier);
        }

        public void Lose()
        {
            _bet.Lose();
        }

        public void Win()
        {
            _wallet.Add(_bet.CollectBank());
        }

        public void TryChangeCardToNewOne(int card)
        {
            if (HasSimulation)
            {
                if (!_wallet.CanSpend(100))
                    return;

                _wallet.Spend(100);

                Simulation.TryChangeCardToNewOne(card);
            }
        }

        public void TopUpAutomaticMarkBonus()
        {
            if (!_wallet.CanSpend(200))
                return;

            _wallet.Spend(200);

            _bonuses.AutomaticMark.TopUp(3);
        }

        public void TopUpHighlithAvailableBonus()
        {
            if (!_wallet.CanSpend(200))
                return;

            _wallet.Spend(200);

            _bonuses.Highlight.TopUp(6);
        }

        public void TopUpMarkMissesBonus()
        {
            if (!_wallet.CanSpend(200))
                return;

            _wallet.Spend(200);

            _bonuses.MarkMisses.TopUp(1);
        }

        public void Visualize(IWalletView view)
        {
            _wallet.Visualize(view);
        }

        public void Visualize(ISimulationView view)
        {
            ReadOnlySimulation.Visualize(view);
        }

        public void Visualize(IWinOrLoseView view)
        {
            if (HasSimulation)
                if (Simulation.IsPlayerWin)
                    view.ShowWin(_bet.Bank);
                else
                    view.ShowLose();
        }

        public void Visualize(IHighlightedCellsView view)
        {
            ReadOnlySimulation.Visualize(view);
        }
    }
}
