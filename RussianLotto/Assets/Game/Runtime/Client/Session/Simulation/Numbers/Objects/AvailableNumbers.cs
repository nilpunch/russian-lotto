using System.Collections.Generic;
using System.Linq;
using RussianLotto.View;

namespace RussianLotto.Client
{
    public class AvailableNumbers : IAvailableNumbers
    {
        private readonly long _unlockingDuration;
        private readonly int _maxAvailableAtOnce;
        private readonly INumbers _numbers;

        private long _startTime;
        private long _currentTime;

        public AvailableNumbers(long unlockingDuration, int maxAvailableAtOnce, INumbers numbers)
        {
            _unlockingDuration = unlockingDuration;
            _maxAvailableAtOnce = maxAvailableAtOnce;
            _numbers = numbers;
            _startTime = -1;
            _currentTime = -1;
        }

        public IEnumerable<int> Available => _numbers.Collection.Where(
            (number, index) => index > CurrentOpenIndex - _maxAvailableAtOnce && index <= CurrentOpenIndex);

        public IEnumerable<int> Missed => _numbers.Collection.Where(
            (number, index) => index <= CurrentOpenIndex - _maxAvailableAtOnce);

        public bool IsEnded => CurrentOpenIndex - _maxAvailableAtOnce >= _numbers.Collection.Count;

        private int CurrentOpenIndex
        {
            get
            {
                if (_startTime == -1)
                    return -1;

                long delta = _currentTime - _startTime;
                int wrappings = (int)(delta / _unlockingDuration);
                return wrappings;
            }
        }

        public void ExecuteFrame(long time)
        {
            if (_startTime == -1)
                _startTime = time;

            _currentTime = time;
        }

        public bool IsAvailable(int number)
        {
            return Available.Contains(number);
        }

        public void Visualize(IAvailableNumbersView view)
        {
            view.DrawAvailableNumbers(_numbers.Collection, CurrentOpenIndex - _maxAvailableAtOnce + 1, _maxAvailableAtOnce);
        }
    }
}
