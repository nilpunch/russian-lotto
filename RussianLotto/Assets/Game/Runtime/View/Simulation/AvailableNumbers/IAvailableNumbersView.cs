using System.Collections.Generic;

namespace RussianLotto.View
{
    public interface IAvailableNumbersView
    {
        public void DrawAvailableNumbers(IReadOnlyList<int> numbers, int from, int amount);
    }
}
