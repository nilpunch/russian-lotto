using BananaParty.BehaviorTree;
using RussianLotto.Application;
using RussianLotto.Networking;
using RussianLotto.View;

namespace RussianLotto.Client
{
    public class LocalClient : IClient, IClientContext
    {
        private readonly IRoom _room;
        private readonly IViewport _viewport;
        private readonly BehaviorNode _behaviorTree;

        public LocalClient(INetwork network, IViewport viewport)
        {
            _viewport = viewport;

            _behaviorTree = new SequenceNode(new IBehaviorNode[]
            {
                new SelectorNode(new IBehaviorNode[]
                {
                    new IsConnectedToServer(network.Socket),
                    new TimeoutNode
                    (
                        new ConnectToServer(network.Socket),
                        5000
                    ),
                    new WaitNode(1000),
                }, true, "ConnectToServer"),

                new SelectorNode(new IBehaviorNode[]
                {
                    new IsConnectedToRoom(network.Room),
                    new TimeoutNode
                    (
                        new ConnectToRandomRoom(network.Room),
                        5000
                    ),
                    new WaitNode(1000),
                }, true, "ConnectToRandomRoom"),

                new SequenceNode(new IBehaviorNode[]
                {
                    new WaitNode(1000),
                }, true, "GameLoop")
            }, true, "ApplicationLoop");
        }

        public void ExecuteFrame(long time)
        {
            if (_behaviorTree.Finished)
                _behaviorTree.Reset();

            _behaviorTree.Execute(time);
        }

        public void Dispose()
        {
        }
    }
}
