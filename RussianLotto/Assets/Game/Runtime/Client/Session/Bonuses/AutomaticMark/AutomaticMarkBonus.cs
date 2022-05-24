using UnityEngine;

namespace RussianLotto.Client
{
    public class AutomaticMarkBonus : Bonus<IAutomaticMark>
    {
        public AutomaticMarkBonus()
        {
        }

        public override void Use(IAutomaticMark target)
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
