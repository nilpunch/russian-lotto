using BananaParty.BehaviorTree;
using RussianLotto.Application;
using RussianLotto.Networking;
using RussianLotto.View;

namespace RussianLotto.Client
{
    public class LocalClient : IClient, IClientContext
    {
        private readonly IRoomNetwork _roomNetwork;
        private readonly IViewport _viewport;
        private readonly BehaviorNode _behaviorTree;

        public LocalClient(ISocket socket, IViewport viewport)
        {
            _roomNetwork = new RoomNetwork(socket);
            _viewport = viewport;

            _behaviorTree = new SequenceNode(new IBehaviorNode[]
            {
                new SelectorNode(new IBehaviorNode[]
                {
                    new IsConnectedToServer(socket),
                    new TimeoutNode
                    (
                        new ConnectToServer(socket),
                        5000
                    ),
                    new WaitNode(1000),
                }, true, "ConnectToServer"),

                new SelectorNode(new IBehaviorNode[]
                {
                    new IsConnectedToRoom(_roomNetwork),
                    new TimeoutNode
                    (
                        new ConnectToRandomRoom(_roomNetwork),
                        5000
                    ),
                    new WaitNode(1000),
                }, true, "ConnectToRandomRoom"),

                new ParallelSequenceNode(new IBehaviorNode[]
                {

                }, "GameLoop")
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
