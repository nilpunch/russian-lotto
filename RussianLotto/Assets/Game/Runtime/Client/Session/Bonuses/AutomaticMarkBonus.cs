using UnityEngine;

namespace RussianLotto.Client
{
    public class AutomaticMarkBonus : Bonus<IAvailableToMarkCells>
    {
        public AutomaticMarkBonus(int initialUses) : base(initialUses)
        {
        }

        public override void Use(IAvailableToMarkCells target)
        {
            int marksAvailable = target.MarksAvailable;

            int canMark = Mathf.Min(UsesLeft, marksAvailable);

            if (canMark > 0)
            {
                UsesLeft -= canMark;
                target.Mark(canMark);
            }
        }
    }
}
