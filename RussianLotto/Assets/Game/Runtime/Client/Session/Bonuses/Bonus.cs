using System;

namespace RussianLotto.Client
{
    public abstract class Bonus<T> : IBonus<T>
    {
        protected Bonus(int initialUses)
        {
            UsesLeft = initialUses;
        }

        public int UsesLeft { get; protected set; }

        public void TopUp(int uses)
        {
            if (uses <= 0)
                throw new ArgumentOutOfRangeException(nameof(uses));

            UsesLeft += uses;
        }

        public abstract void Use(T target);
    }
}
