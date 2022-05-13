﻿using System.Collections.Generic;
using System.Linq;
using RussianLotto.Client;
using UnityEngine;

namespace RussianLotto.View
{
    public class BoardView : MonoBehaviour, IBoardView
    {
        [SerializeField] private MissingNumberView _missingNumberView;
        [SerializeField] private CardView[] _cards;

        public void DrawCards(IReadOnlyCollection<IReadOnlyCard> cards)
        {
            foreach ((IReadOnlyCard card, CardView view) in cards.Zip(_cards, (card, view) => (card, view)))
                view.DrawCells(card.Cells);
        }

        public void DrawLastMissingNumber(int lastMissingNumber)
        {
            //_missingNumberView.Show(lastMissingNumber);
        }

        public void HideLastMissingNumber()
        {
            //_missingNumberView.Hide();
        }
    }
}
