namespace RussianLotto.Client
{
    public interface IOfflineMoneyEarn
    {
        bool HasEarn { get; }
        void Collect(IWallet wallet);
    }
}
