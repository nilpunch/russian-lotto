using System.Collections.Generic;
using System.Linq;
using RussianLotto.Client;
using UnityEngine;

namespace RussianLotto.View
{
    public class BoardView : MonoBehaviour, IBoardView
    {
        [field: SerializeField] public CardView[] Cards { get; private set; } = null;

        public void DrawCards(IReadOnlyCollection<IReadOnlyCard> cards)
        {
            foreach ((IReadOnlyCard card, CardView view) in cards.Zip(Cards, (card, view) => (card, view)))
                view.DrawCells(card.Cells);
        }
    }
}
