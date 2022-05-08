using System.Linq;

namespace RussianLotto.Client
{
    public class BoardFactory : IFactory<IBoard>
    {
        private readonly int _cardsAmount;
        private readonly IFactory<ICard> _cardFactory;

        public BoardFactory(int cardsAmount, IFactory<ICard> cardFactory)
        {
            _cardsAmount = cardsAmount;
            _cardFactory = cardFactory;
        }

        public IBoard Create()
        {
            ICard[] cards = Enumerable.Range(0, _cardsAmount).Select(i => _cardFactory.Create()).ToArray();

            return new Board(cards);
        }
    }
}
