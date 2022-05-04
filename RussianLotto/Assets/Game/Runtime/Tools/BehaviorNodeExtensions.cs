using BananaParty.BehaviorTree;

namespace RussianLotto.Tools
{
    public static class BehaviorNodeExtensions
    {
        public static IBehaviorNode Invert(this IBehaviorNode behaviorNode)
        {
            return new InverterNode(behaviorNode);
        }
    }
}
