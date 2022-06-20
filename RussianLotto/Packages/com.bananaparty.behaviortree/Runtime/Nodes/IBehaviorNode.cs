namespace BananaParty.BehaviorTree
{
    public interface IBehaviorNode : IReadOnlyBehaviorNode
    {
        BehaviorNodeStatus Execute(long time);
        void Reset();
    }
}
