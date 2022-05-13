using System.Collections.Generic;
using RussianLotto.View;

namespace RussianLotto.Client
{
    public interface IReadOnlyAvailableNumbers : IVisualization<IAvailableNumbersView>
    {
        IEnumerable<int> Available { get; }
        IEnumerable<int> Missed { get; }

        bool IsEnded { get; }

        bool IsAvailable(int number);
    }
}
