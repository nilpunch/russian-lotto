using UnityEngine;

namespace RussianLotto.Client
{
    public class MarkMissesBonus : Bonus<IBoard>
    {
        public MarkMissesBonus()
        {
        }

        public override void Use(IBoard target)
        {
            int availableToRestore = target.MissesAmount;

            int canRestore = Mathf.Min(UsesLeft, availableToRestore);

            if (canRestore > 0)
            {
                UsesLeft -= canRestore;
                target.RestoreMisses(canRestore);
            }
        }
    }
}
