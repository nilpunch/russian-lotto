using RussianLotto.Save;
using RussianLotto.View;

namespace RussianLotto.Client
{
    public interface IWallet : ISerialization, IDeserialization, IVisualization<IWalletView>
    {
        public void Add(int amount);

        public bool CanSpend(int amount);

        public void Spend(int amount);
    }
}
