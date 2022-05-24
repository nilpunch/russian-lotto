namespace RussianLotto.Client
{
    public interface IClient : IGameLoop
    {

    }

    public interface IShop
    {
        public bool TryBet(int bet, IWallet wallet);
        public bool TryBuyReroll(IWallet wallet);
        public bool TryBuyBonus(BonusType bonusType, IWallet wallet);
    }

    public interface IItem
    {
        int Cost { get; }

        void Purchase();
    }
}
