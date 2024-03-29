﻿using BananaParty.BehaviorTree;

namespace RussianLotto.Tools
{
    public static class BehaviorNodeExtensions
    {
        public static IBehaviorNode Invert(this IBehaviorNode behaviorNode)
        {
            return new InverterNode(behaviorNode);
        }

        public static IBehaviorNode Repeat(this IBehaviorNode behaviorNode)
        {
            return new RepeatNode(behaviorNode);
        }

        public static IBehaviorNode RepeatUntil(this IBehaviorNode behaviorNode, BehaviorNodeStatus? behaviorNodeStatus)
        {
            return new RepeatNode(behaviorNode, behaviorNodeStatus);
        }
    }
}
