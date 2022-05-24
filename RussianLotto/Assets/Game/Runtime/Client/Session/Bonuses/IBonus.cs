using RussianLotto.Save;

namespace RussianLotto.Client
{
    public interface IBonus<in T> : ISerialization, IDeserialization
    {
        public int UsesLeft { get; }

        public void TopUp(int uses);

        public void Use(T target);
    }
}
