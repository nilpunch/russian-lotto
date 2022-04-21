using System.Collections.Generic;

namespace RussianLotto.Client
{
    public class Room
    {
        private readonly int _seed;

        public Room(int seed)
        {
            _seed = seed;
        }

        public LocalLottoSession CreateLocalSession()
        {
            var rnd = new RandomNumberGenerator(_seed);
            var numbersGenerator = new NumbersFactory(90, rnd);
            var numbers = numbersGenerator.Create();

            var cardFactory = new ClassicCardFactory();
            var boardFactory = new BoardFactory(4, cardFactory);
            var board = boardFactory.Create();

            var availableNumbers = new AvailableNumbers(3, 5, numbers);
            return new LocalLottoSession(board, availableNumbers);
        }
    }
}
