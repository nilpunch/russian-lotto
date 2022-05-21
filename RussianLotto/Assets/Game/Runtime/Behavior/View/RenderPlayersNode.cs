using BananaParty.BehaviorTree;
using RussianLotto.Client;
using RussianLotto.Networking;
using RussianLotto.View;

namespace RussianLotto.Behavior
{
    public class RenderPlayersNode : BehaviorNode
    {
        private readonly IPlayersView _view;
        private readonly IPlayers _players;

        public RenderPlayersNode(IPlayersView view, IPlayers players)
        {
            _view = view;
            _players = players;
        }

        public override BehaviorNodeStatus OnExecute(long time)
        {
            _players.Visualize(_view);
            return BehaviorNodeStatus.Success;
        }
    }
}
