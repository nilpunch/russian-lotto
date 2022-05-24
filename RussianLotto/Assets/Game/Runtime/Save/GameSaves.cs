using RussianLotto.Client;

namespace RussianLotto.Save
{
    public class GameSaves : IGameSaves
    {
        public GameSaves()
        {
            WalletSave = new FileSave<IWallet,Wallet>();
            BonusesSave = new FileSave<IBonuses,Bonuses>();
        }

        public ISave<IWallet> WalletSave { get; }

        public ISave<IBonuses> BonusesSave { get; }
    }
}
