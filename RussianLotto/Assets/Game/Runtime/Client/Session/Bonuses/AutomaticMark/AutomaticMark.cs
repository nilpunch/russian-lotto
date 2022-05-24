using System;

namespace RussianLotto.Client
{
    public class AutomaticMark : IAutomaticMark
    {
        private readonly IBoard _board;
        private readonly IReadOnlyAvailableNumbers _availableNumbers;

        public AutomaticMark(IBoard board, IReadOnlyAvailableNumbers availableNumbers)
        {
            _board = board;
            _availableNumbers = availableNumbers;
        }

        public int MarksAvailable => _board.AvailableToMark(_availableNumbers);

        public void Mark(int amount)
        {
            if (amount > MarksAvailable)
                throw new InvalidOperationException();

            _board.MarkAvailableNumbers(_availableNumbers, amount);
        }
    }
}