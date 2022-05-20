using NUnit.Framework;
using RussianLotto.Client;
using UnityEngine;

namespace RussianLotto.Tests
{
    public class GenerationTests
    {
        // [Test]
        // public void DisplayGeneratedCard()
        // {
        //
        //     for (int seed = 100; seed < 200; seed++)
        //     {
        //         var rng = new RandomNumberGenerator(seed);
        //         var cardFactory = new ClassicCardFactory(rng, true);
        //
        //         DisplayCard(cardFactory.Create());
        //     }
        // }

        private void DisplayCard(IReadOnlyCard card)
        {
            string[,] numbers = new string[3,9];

            for (int y = 0; y < 3; ++y)
            {
                for (int x = 0; x < 9; ++x)
                {
                    numbers[y, x] = 0.ToString();
                }
            }

            foreach (var cell in card.ReadOnlyCells)
                numbers[cell.Position.y, cell.Position.x] = cell.Number.ToString();

            var tabbedData = StringHelp.EvenColumns(3, StringHelp.ToJaggedArray(numbers));
            Debug.Log(tabbedData);
        }
    }
}
