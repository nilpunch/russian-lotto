using System.Collections.Generic;

namespace RussianLotto.Client
{
    public interface INumbers
    {
        IReadOnlyCollection<int> Collection { get; }
    }
}
