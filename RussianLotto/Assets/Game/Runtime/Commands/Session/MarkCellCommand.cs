using RussianLotto.Client;
using UnityEngine;

namespace RussianLotto.Command
{
    public class MarkCellCommand : ISessionCommand
    {
        private readonly int _card;
        private readonly Vector2Int _cell;

        public MarkCellCommand(int card, Vector2Int cell)
        {
            _card = card;
            _cell = cell;
        }

        public void Execute(ISession target)
        {
            target.Simulation.TryMarkCell(_card, _cell);
        }
    }
}
