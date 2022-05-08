using System.Collections.Generic;
using RussianLotto.View;

namespace RussianLotto.Client
{
    public class AvailableNumbers : IAvailableNumbers
    {
        private readonly long _unlockingDuration;
        private readonly int _maxAvailableNumbers;
        private readonly INumbers _numbers;

        private long _startTime;
        private long _currentTime;

        public AvailableNumbers(long unlockingDuration, int maxAvailableNumbers, INumbers numbers)
        {
            _unlockingDuration = unlockingDuration;
            _maxAvailableNumbers = maxAvailableNumbers;
            _numbers = numbers;
            _startTime = -1;
            _currentTime = -1;
        }

        public IEnumerable<int> Available { get; }
        public IEnumerable<int> Missed { get; }

        private int CurrentOpenIndex
        {
            get
            {
                long delta = _currentTime - _startTime;
                int wrappings = (int)(delta / _unlockingDuration);
                return wrappings - 1;
            }
        }

        public void ExecuteFrame(long time)
        {
            // if (_startTime == -1)
            //     _startTime = time;
            //
            // _currentTime = time;
            //
            // if (time < _startTime + _unlockingDuration)
            //     return;
            //
            // long delta = time - _startTime;
            // long wrappings = delta / _unlockingDuration;
            //
            // _startTime += _unlockingDuration * wrappings;
            //
            // for (int i = 0; i < wrappings; ++i)
            // {
            //     _availableNumbers.OpenNext();
            // }
        }

        public bool IsAvailable(int number)
        {
            return false;
            for (int i = 0; i < _maxAvailableNumbers; ++i)
            {

            }
        }

        public void Visualize(IAvailableNumbersView view)
        {
            view.DrawAvailableNumbers(Available);
        }
    }
}
