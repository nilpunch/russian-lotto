using System;
using RussianLotto.Client;

namespace RussianLotto.Command
{
    public class TopUpBonusCommand : ISessionCommand
    {
        private readonly BonusType _bonusType;

        public TopUpBonusCommand(BonusType bonusType)
        {
            _bonusType = bonusType;
        }

        public void Execute(ISession target)
        {
            switch (_bonusType)
            {
                case BonusType.AutomaticMark:
                    target.TopUpAutomaticMarkBonus();
                    break;
                case BonusType.MarkMisses:
                    target.TopUpMarkMissesBonus();
                    break;
                case BonusType.HighlightAvailable:
                    target.TopUpHighlithAvailableBonus();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
