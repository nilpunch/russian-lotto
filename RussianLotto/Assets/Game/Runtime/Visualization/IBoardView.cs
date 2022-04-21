using System.Collections.Generic;
using RussianLotto.Client;

namespace RussianLotto.View
{
    public interface IBoardView
    {
        void DrawCards(IReadOnlyCollection<IReadOnlyCard> cards);
    }
}
