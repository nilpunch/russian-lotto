using RussianLotto.Client;

namespace RussianLotto.Save
{
    public interface IGameSaves
    {
        ISave<IWallet> WalletSave { get; }

        ISave<IBonuses> BonusesSave { get; }
    }
}
