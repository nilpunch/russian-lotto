using System;
using RussianLotto.Save;

namespace RussianLotto.Client
{
    public abstract class Bonus<T> : IBonus<T>
    {
        public int UsesLeft { get; protected set; }

        public void TopUp(int uses)
        {
            if (uses <= 0)
                throw new ArgumentOutOfRangeException(nameof(uses));

            UsesLeft += uses;
        }

        public abstract void Use(T target);

        public void Serialize(IWriteHandle writeHandle)
        {
            writeHandle.WriteInt(UsesLeft);
        }

        public void Deserialize(IReadHandle readHandle)
        {
            UsesLeft = readHandle.ReadInt();
        }
    }
}
