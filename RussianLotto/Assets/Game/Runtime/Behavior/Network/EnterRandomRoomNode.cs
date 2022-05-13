using BananaParty.BehaviorTree;
using RussianLotto.Client;
using RussianLotto.Input;
using RussianLotto.Networking;

namespace RussianLotto.Behavior
{
    public class EnterRandomRoomNode : BehaviorNode
    {
        private readonly IRoom _room;
        private readonly ISwitch<bool> _shuffleMode;
        private readonly ISwitch<GameType> _gameType;

        private bool _connectCalledOnce;

        public EnterRandomRoomNode(IRoom room, ISwitch<bool> shuffleMode, ISwitch<GameType> gameType)
        {
            _room = room;
            _shuffleMode = shuffleMode;
            _gameType = gameType;
        }

        public override BehaviorNodeStatus OnExecute(long time)
        {
            if (_connectCalledOnce)
            {
                return _room.IsEntered ? BehaviorNodeStatus.Success : BehaviorNodeStatus.Running;
            }

            if (_room.CanSendCommands == false)
            {
                return BehaviorNodeStatus.Running;
            }

            _connectCalledOnce = true;
            _room.EnterRandom(_gameType.State, _shuffleMode.State);
            return BehaviorNodeStatus.Running;
        }

        public override void Reset()
        {
            base.Reset();
            _connectCalledOnce = false;
        }
    }
}
