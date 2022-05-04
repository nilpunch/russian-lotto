using System.Collections.Generic;
using UnityEngine;

namespace RussianLotto.Client
{
    public class Card : ICard
    {
        public Card(IReadOnlyCollection<IReadOnlyCell> cells)
        {
            Cells = cells;
        }

        public IReadOnlyCollection<IReadOnlyCell> Cells { get; }

        public bool IsAvailable(Vector2Int cell)
        {
            throw new System.NotImplementedException();
        }

        public void Mark(Vector2Int cell)
        {
            throw new System.NotImplementedException();
        }

        public void Miss(Vector2Int cell)
        {
            throw new System.NotImplementedException();
        }
    }
}
