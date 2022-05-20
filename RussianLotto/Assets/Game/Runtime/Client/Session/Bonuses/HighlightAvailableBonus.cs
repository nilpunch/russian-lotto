using UnityEngine;

namespace RussianLotto.Client
{
    public class HighlightAvailableBonus : Bonus<IHighlightedCells>
    {
        public HighlightAvailableBonus(int initialUses) : base(initialUses)
        {
        }

        public override void Use(IHighlightedCells target)
        {
            int availableHighlights = target.HighlightsAvailable;

            int canHighlight = Mathf.Min(UsesLeft, availableHighlights);

            if (canHighlight > 0)
            {
                UsesLeft -= canHighlight;
                target.Highlight(canHighlight);
            }
        }
    }
}
