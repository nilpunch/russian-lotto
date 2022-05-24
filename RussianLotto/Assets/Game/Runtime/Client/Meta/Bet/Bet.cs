namespace RussianLotto.Client
{
    public class Bet : IBet
    {
        public int Bank { get; private set; }

        public void Add(int bet)
        {
            Bank += bet;
        }

        public void Multiply(int multiplier)
        {
            Bank *= multiplier;
        }

        public void Lose()
        {
            Bank = 0;
        }

        public int CollectBank()
        {
            int bank = Bank;
            Bank = 0;
            return bank;
        }
    }
}
