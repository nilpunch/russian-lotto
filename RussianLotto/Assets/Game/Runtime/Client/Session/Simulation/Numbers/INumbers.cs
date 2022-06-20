using System.Collections.Generic;

namespace RussianLotto.Client
{
    public interface INumbers
    {
        IReadOnlyList<int> Collection { get; }
    }
}
