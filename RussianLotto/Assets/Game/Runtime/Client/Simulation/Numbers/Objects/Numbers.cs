using System.Collections.Generic;

namespace RussianLotto.Client
{
    public class Numbers : INumbers
    {
        public Numbers(IReadOnlyCollection<int> collection)
        {
            Collection = collection;
        }

        public IReadOnlyCollection<int> Collection { get; }
    }
}
