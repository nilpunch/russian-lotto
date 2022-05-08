using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RussianLotto.Client
{
    public class CardFactory : IFactory<ICard>
    {
        private const int Width = 9;
        private const int Height = 3;
        private const int NumbersPerRaw = 5;

        private readonly IRandomNumberGenerator _randomNumberGenerator;
        private readonly bool _shuffled;
        private readonly int _maxNumber;

        protected CardFactory(IRandomNumberGenerator randomNumberGenerator, bool shuffled, int maxNumber)
        {
            _randomNumberGenerator = randomNumberGenerator;
            _shuffled = shuffled;
            _maxNumber = maxNumber;
        }

        public ICard Create()
        {
            var cells = new Cell[NumbersPerRaw * Height];
            int cellsCounter = 0;

            int numbersAmount = _maxNumber;
            int[] allNumbers = Enumerable.Range(1, numbersAmount).ToArray();

            if (_shuffled)
            {
                allNumbers.Shuffle(_randomNumberGenerator.Range);

                int[] rowPositions = Enumerable.Range(0, Width).ToArray();
                for (int y = 0; y < Height; ++y)
                {
                    rowPositions.Shuffle(_randomNumberGenerator.Range);
                    for (int x = 0; x < NumbersPerRaw; ++x)
                    {
                        Vector2Int cellPosition = new(rowPositions[x], y);
                        cells[cellsCounter] = new Cell(allNumbers[cellsCounter], cellPosition, CellStatus.Available);
                        cellsCounter += 1;
                    }
                }
            }
            else
            {
                List<int>[] allNumbersByDigit = new List<int>[Width];

                for (int i = 0; i < Width; ++i)
                {
                    allNumbersByDigit[i] = new List<int>();
                }

                int divider = _maxNumber / Width;

                for (int i = 0; i < numbersAmount; ++i)
                {
                    int number = allNumbers[i];
                    int numberDigit = number == _maxNumber ? Width - 1 : number / divider;
                    allNumbersByDigit[numberDigit].Add(number);
                }

                int[] rowPositions = Enumerable.Range(0, Width).ToArray();
                for (int y = 0; y < Height; ++y)
                {
                    rowPositions.Shuffle(_randomNumberGenerator.Range);
                    for (int x = 0; x < NumbersPerRaw; ++x)
                    {
                        int digit = rowPositions[x];
                        int number = allNumbersByDigit[digit].Shuffle()[0];
                        allNumbersByDigit[digit].Remove(number);

                        Vector2Int cellPosition = new(digit, y);
                        cells[cellsCounter] = new Cell(number, cellPosition, CellStatus.Available);
                        cellsCounter += 1;
                    }
                }
            }

            return new Card(cells);
        }
    }
}
