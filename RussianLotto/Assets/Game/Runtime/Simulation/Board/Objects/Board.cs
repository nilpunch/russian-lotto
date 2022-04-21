using System.Collections.Generic;
using RussianLotto.View;
using UnityEngine;

namespace RussianLotto.Client
{
    public class Board : IBoard
    {
        private readonly IReadOnlyCollection<ICard> _cards;

        public Board(IReadOnlyCollection<ICard> cards)
        {
            _cards = cards;
        }

        public bool IsAvailable(int card, Vector2Int cellPosition)
        {
            throw new System.NotImplementedException();
        }

        public void Mark(int card, Vector2Int cellPosition)
        {
            throw new System.NotImplementedException();
        }

        public void Miss(int card, Vector2Int cellPosition)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateAllMissingNumbers(IReadOnlyAvailableNumbers availableNumbers)
        {
            throw new System.NotImplementedException();
        }

        public void Visualize(IBoardView view)
        {
            throw new System.NotImplementedException();
        }
    }
}
