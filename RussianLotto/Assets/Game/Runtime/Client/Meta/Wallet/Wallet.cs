using System;
using RussianLotto.Save;
using RussianLotto.View;

namespace RussianLotto.Client
{
    public class Wallet : IWallet
    {
        private int _moneys;

        public Wallet()
        {
            _moneys = 10000;
        }

        public void Add(int amount)
        {
            if (amount < 0)
                throw new ArgumentOutOfRangeException(nameof(amount));

            _moneys += amount;
        }

        public bool CanSpend(int amount)
        {
            if (amount < 0)
                throw new ArgumentOutOfRangeException(nameof(amount));

            return _moneys >= amount;
        }

        public void Spend(int amount)
        {
            if (!CanSpend(amount))
                throw new InvalidOperationException(nameof(amount));

            _moneys -= amount;
        }

        public void Serialize(IWriteHandle writeHandle)
        {
            writeHandle.WriteInt(_moneys);
        }

        public void Deserialize(IReadHandle readHandle)
        {
            _moneys = 10000;
            //_moneys = readHandle.ReadInt();
        }

        public void Visualize(IWalletView view)
        {
            view.ShowMoneys(_moneys);
        }
    }
}
