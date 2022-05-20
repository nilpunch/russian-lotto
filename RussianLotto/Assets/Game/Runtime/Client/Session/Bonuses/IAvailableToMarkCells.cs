using System;

namespace RussianLotto.Client
{
    public interface IAvailableToMarkCells
    {
        public int MarksAvailable { get; }
        public void Mark(int amount);
    }

    public class AvailableToMarkCells : IAvailableToMarkCells
    {
        private readonly IBoard _board;
        private readonly IReadOnlyAvailableNumbers _availableNumbers;

        public AvailableToMarkCells(IBoard board, IReadOnlyAvailableNumbers availableNumbers)
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
