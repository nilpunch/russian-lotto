using System.Collections.Generic;

namespace RussianLotto.Client
{
    public class Numbers : INumbers
    {
        public Numbers(IReadOnlyList<int> collection)
        {
            Collection = collection;
        }

        public IReadOnlyList<int> Collection { get; }
    }
}
