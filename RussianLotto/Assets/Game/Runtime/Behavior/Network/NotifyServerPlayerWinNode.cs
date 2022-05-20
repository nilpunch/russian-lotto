using BananaParty.BehaviorTree;
using RussianLotto.Master;
using RussianLotto.Networking;

namespace RussianLotto.Behavior
{
    public class NotifyServerPlayerWinNode : BehaviorNode
    {
        private readonly IRoom _room;

        public NotifyServerPlayerWinNode(IRoom room)
        {
            _room = room;
        }
        
        public override BehaviorNodeStatus OnExecute(long time)
        {
            if (Started)
                return Status;
            
            _room.SendToServer(new FinishMasterGameCommand());
            
            return BehaviorNodeStatus.Success;
        }
    }
}
