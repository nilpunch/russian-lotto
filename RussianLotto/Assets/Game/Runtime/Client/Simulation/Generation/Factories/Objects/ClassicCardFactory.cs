using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RussianLotto.Client
{
    public class ClassicCardFactory : CardFactory
    {
        public ClassicCardFactory(IRandomNumberGenerator randomNumberGenerator, bool shuffled) : base(randomNumberGenerator, shuffled, 90)
        {
        }
    }
}
