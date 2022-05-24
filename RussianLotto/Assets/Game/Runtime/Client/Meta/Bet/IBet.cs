namespace RussianLotto.Client
{
    public interface IBet
    {
        public int Bank { get; }

        public void Add(int bet);
        public void Multiply(int multiplier);
        public void Lose();
        public int CollectBank();
    }
}
