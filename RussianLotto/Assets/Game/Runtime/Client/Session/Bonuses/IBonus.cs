namespace RussianLotto.Client
{
    public interface IBonus<in T>
    {
        public int UsesLeft { get; }

        public void TopUp(int uses);

        public void Use(T target);
    }
}
