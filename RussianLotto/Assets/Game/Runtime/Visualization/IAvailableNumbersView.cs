using System.Collections.Generic;

namespace RussianLotto.View
{
    public interface IAvailableNumbersView
    {
        public void DrawAvailableNumbers(IEnumerable<int> numbers);
    }
}
